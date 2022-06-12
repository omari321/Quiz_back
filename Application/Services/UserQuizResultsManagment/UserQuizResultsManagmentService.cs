using Infrastructure.Entities.QuizResults;
using Infrastructure.Entities.QuizResults.Dtos;
using Infrastructure.Entities.User;
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

namespace Application.Services.UserQuizResultsManagment
{
    public class UserQuizResultsManagmentService : IUserQuizResultsManagmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<QuizResultsEntity>  _quizResultEntityRepository;
        private readonly IGenericRepository<UserEntity> _userRepository;

        public UserQuizResultsManagmentService(IUnitOfWork unitOfWork, IGenericRepository<QuizResultsEntity> quizResultEntityRepository, IGenericRepository<UserEntity> userRepository)
        {
            _unitOfWork = unitOfWork;
            _quizResultEntityRepository = quizResultEntityRepository;
            _userRepository = userRepository;
        }

        public async Task<PageReturnDto<QuizResultDto>> GetUserResults(QueryParams model)
        {
           var QuizResults= await  _quizResultEntityRepository
                .Query()
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Include(x=>x.User)
                .Include(x=>x.Quiz)
                .ToListAsync();
            var count = await _quizResultEntityRepository.Query().CountAsync();
            var QuizResultDto = QuizResults.Select(x =>
              {
                  return new QuizResultDto(x.User.Id,x.User.UserName,x.QuizId,x.Quiz.QuizName,x.CorrectAnswers,x.TotalQuestions,x.CreatedAt);
              });
            return new PageReturnDto<QuizResultDto>(QuizResultDto, count, model.Page, model.ItemsPerPage);
        }

        public async Task<PageReturnDto<QuizResultDto>> GetUserResultsForUser(int userId,QueryParams model)
        {
            var checkUser = await _userRepository.FindOneByConditionAsync(x => x.Id == userId);
            if (checkUser is null)
            {
                throw new CustomException("this user does not exist", 404);
            }
            var QuizResults = await _quizResultEntityRepository
                         .Query()
                         .Where(x=>x.UserId==userId)
                         .Skip((model.Page - 1) * model.ItemsPerPage)
                         .Take(model.ItemsPerPage)
                         .Include(x => x.User)
                         .Include(x => x.Quiz)
                         .ToListAsync();
            var count = await _quizResultEntityRepository.Query().CountAsync();
            var QuizResultDto = QuizResults.Select(x =>
            {
                return new QuizResultDto(x.User.Id, x.User.UserName, x.QuizId, x.Quiz.QuizName, x.CorrectAnswers, x.TotalQuestions, x.CreatedAt);
            });
            return new PageReturnDto<QuizResultDto>(QuizResultDto, count, model.Page, model.ItemsPerPage);
        }
    }
}
