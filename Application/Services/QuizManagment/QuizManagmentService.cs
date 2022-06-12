using Infrastructure.Entities.Answers.Dtos;
using Infrastructure.Entities.Questions;
using Infrastructure.Entities.Quiz;
using Infrastructure.Entities.Quiz.Dtos;
using Infrastructure.Entities.QuizQuotes;
using Infrastructure.Entities.QuizResults;
using Infrastructure.Entities.Quote.Dtos;
using Infrastructure.Entities.User;
using Infrastructure.Entities.User.Dtos;
using Infrastructure.Enums;
using Infrastructure.Paging;
using Infrastructure.RepositoryRelated;
using Infrastructure.UnitOfWorkRelated;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.QuizManagment
{
    public class QuizManagmentService : IQuizManagmentService
    {
        private readonly IGenericRepository<QuizEntity> _quizEntityRepository;
        private readonly IGenericRepository<QuotesEntity> _quotesEntityRepository;
        private readonly IGenericRepository<QuizQuotesEntity> _quizQuotesEntityRepository;
        private readonly IGenericRepository<UserEntity> _userRepository;
        private readonly IGenericRepository<QuizResultsEntity> _quizResultRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuizManagmentService(IGenericRepository<QuizResultsEntity> quizResultRepository,IGenericRepository<UserEntity> userRepository, IGenericRepository<QuizEntity> quizEntityRepository, IGenericRepository<QuotesEntity> quotesEntityRepository, IGenericRepository<QuizQuotesEntity> quizQuotesEntityRepository, IUnitOfWork unitOfWork)
        {
            _quizEntityRepository = quizEntityRepository;
            _quotesEntityRepository = quotesEntityRepository;
            _quizQuotesEntityRepository = quizQuotesEntityRepository;
            _userRepository = userRepository;
            _quizResultRepository = quizResultRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<QuizQuotesEntity>> AddQuotesToQuiz(AddQuoteToQuizDto model)
        {
            var quiz = await _quizEntityRepository.Query()
                .Where(x => x.Id == model.QuizId)
                .Include(x=>x.QuizQuotes)
                .FirstOrDefaultAsync();
            if (quiz is null)
            {
                throw new CustomException("Quit with this id does not exist ", 404);
            }
            var quizQuotes =new List<QuizQuotesEntity>();
            foreach(var i in model.QuoteIds)
            {
                if (! await _quotesEntityRepository.Query().AnyAsync(x => x.Id==i))
                {
                    throw new CustomException("quote with this id does not exist", 404);
                }
                quizQuotes.Add(new QuizQuotesEntity
                {
                    QuizId = model.QuizId,
                    QuoteId = i,
                    CreatedAt = DateTime.Now
                });
            }
            quiz.QuizQuotes=quizQuotes;
            await _unitOfWork.CompleteAsync();
            return quizQuotes;
        }
        public async Task<QuizDto> CreateQuiz(CreateQuizDto model)
        {
            if (await _quizEntityRepository.Query().AnyAsync(x=>x.QuizName==model.QuizName))
            {
                throw new CustomException("quiz with this name already exists", 403);
            }
            var newQuiz = new QuizEntity
            {
                QuizName = model.QuizName,
                CreatedAt = DateTime.Now,
                IsDisabled=false
            };
            await _quizEntityRepository.AddAsync(newQuiz);
            await _unitOfWork.CompleteAsync();
            return new QuizDto(newQuiz.Id,newQuiz.QuizName);
        }

        public async Task<bool> DisableQuiz(int QuizId)
        {
            if (!await _quizEntityRepository.Query().AnyAsync(x => x.Id == QuizId))
            {
                throw new CustomException("quiz with this Id does not exists", 403);
            }
            var quiz = await _quizEntityRepository.FindOneByConditionAsync(x => x.Id == QuizId);
            quiz.IsDisabled = true;
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<PageReturnDto<QuizDto>> GetDisabledQuizes(QueryParams model)
        {
            var quizes = await _quizEntityRepository.Query()
                           .Where(x=>x.IsDisabled==true)
                           .Skip((model.Page - 1) * model.ItemsPerPage)
                           .Take(model.ItemsPerPage)
                           .ToListAsync();
            var count = await _quizEntityRepository.Query()
                    .Where(x => x.IsDisabled == true)
                    .CountAsync();

            var quizDtos = quizes.Select(x =>
            {
                return new QuizDto(x.Id, x.QuizName);
            });
            return new PageReturnDto<QuizDto>(quizDtos, count, model.Page, model.ItemsPerPage);
        }

        public async Task<PageReturnDto<QuizDto>> GetQuizes(QuizParameters model)
        {
            var quizes=await _quizEntityRepository.Query()
                            .Where(x=>x.QuizName.Contains(model.SearchTerm))
                            .Skip((model.Page - 1) * model.ItemsPerPage)
                            .Take(model.ItemsPerPage)
                            .ToListAsync();
            var count = await _quizEntityRepository.Query()
                .Where(x => x.QuizName .Contains( model.SearchTerm))
                .CountAsync();

            var quizDtos = quizes.Select(x =>
              {
                  return new QuizDto(x.Id,x.QuizName);
              });
            return new PageReturnDto<QuizDto>(quizDtos,count,model.Page,model.ItemsPerPage);
        }

        public async Task<IEnumerable<QuoteDto>> LoadQuizQuotes(int userId,int QuizId, QuizType quizType)
        {
            var user = await _userRepository.FindOneByConditionAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new CustomException("user with this id does not exist", 404);
            }
            else if (user.IsDisabled!=null && (bool)user.IsDisabled)
            {
                throw new CustomException("this user is disabled", 403);
            }
            var quizQuotes = await _quizQuotesEntityRepository.Query()
                            .Where(x => x.QuizId == QuizId)
                            .Include(x => x.Quote)
                            .ThenInclude(x=>x.answersEntities)
                            .ToListAsync();
            var random = new Random();
            if (quizType == QuizType.MultipleChoice)
            {

                var quoteDtos = quizQuotes.Select(x =>
                  {
                      var answersList = new List<AnswersDto>();
                      foreach (var i in x.Quote.answersEntities)
                      {
                          answersList.Add(new AnswersDto(i.Answer, i.IsCorrect));
                      }
                      return new QuoteDto(x.Quote.Id, x.Quote.Quote, answersList);
                  });
                return quoteDtos;
            }
            else
            {
                var quoteDtos = quizQuotes.Select(x =>
                {
                    var answersList = new List<AnswersDto>();
                    var index=random.Next(x.Quote.answersEntities.Count);
                    var i = x.Quote.answersEntities[index];
                    answersList.Add(new AnswersDto(i.Answer, i.IsCorrect));
                    return new QuoteDto(x.Quote.Id, x.Quote.Quote, answersList);
                });
                return quoteDtos;
            }
            
        }

        public async Task<bool> SubmitQuiz(int userId, QuizSubmitDto model)
        {
            var user = await _userRepository.FindOneByConditionAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new CustomException("user with this id does not exist", 404);
            }
            var quiz = await _quizEntityRepository.FindOneByConditionAsync(x => x.Id == model.quizId);
            ushort correct = 0;
            ushort Total = 0;
            foreach(var i in model.answers)
            {
                if (i)
                {
                    correct++;
                }
                Total++;
            }
            var quizResult = new QuizResultsEntity
            {
                UserId= userId,
                QuizId=model.quizId,
                QuizType=model.quizType,
                CorrectAnswers=correct,
                TotalQuestions=Total,
                CreatedAt=DateTime.Now,
            };
            await  _quizResultRepository.AddAsync(quizResult);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
