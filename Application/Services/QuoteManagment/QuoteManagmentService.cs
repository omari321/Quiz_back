using Infrastructure.Entities.Answers;
using Infrastructure.Entities.Answers.Dtos;
using Infrastructure.Entities.Questions;
using Infrastructure.Entities.Questions.Dtos;
using Infrastructure.Entities.Quote.Dtos;
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

namespace Application.Services.QuoteManagment
{
    public class QuoteManagmentService : IQuoteManagementService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IGenericRepository<QuotesEntity> _quotesRepository;

        public QuoteManagmentService(IUnitOfWork unitOfWork, IGenericRepository<QuotesEntity> quotesRepository)
        {
            _UnitOfWork = unitOfWork;
            _quotesRepository = quotesRepository;
        }

        public async Task<QuoteDto> CreteQuote(CreateQuoteDto model)
        {
            var Quote = new QuotesEntity
            {
                Quote=model.Quote,
                CreatedAt=DateTime.Now,
            };
            var answers = new List<AnswersEntity>();
            var totalTrue = 0;
            foreach(var i in model.answers)
            {
                if (i.IsCorrect)
                {
                    totalTrue++;
                }
                answers.Add(new AnswersEntity
                {
                    Answer=i.answer,
                    IsCorrect = i.IsCorrect,
                    CreatedAt=DateTime.Now,
                });
            }
            if (totalTrue!=1)
            {
                throw new CustomException("there can only be 1 true answer", 400);
            }
            Quote.answersEntities = answers;
            await _quotesRepository.AddAsync(Quote);
            await _UnitOfWork.CompleteAsync();
            return new QuoteDto
            {
                Id = Quote.Id,
                Quote = model.Quote,
                Answers = model.answers
            };
        }

        public async Task<QuotesEntity> DeleteQuote(int QuoteId)
        {
            var entity = await _quotesRepository.FindOneByConditionAsync(x => x.Id == QuoteId);
            if (entity is null)
            {
                throw new CustomException("quote with this id does not exist", 404);
            }
            _quotesRepository.Remove(entity);
            await _UnitOfWork.CompleteAsync();
            return entity;
        }

        public async Task<QuoteDto> GetQuoteById(int QuoteId)
        {
            var entity = await _quotesRepository
                .Query()
                .Where(x => x.Id == QuoteId)
                .Include(x=>x.answersEntities)
                .FirstOrDefaultAsync();
            if (entity is null)
            {
                throw new CustomException("quote with this id does not exist", 404);
            }
            return new QuoteDto
            {
                Id = entity.Id,
                Quote = entity.Quote,
                Answers = entity.answersEntities.Select(x =>
                  {
                      return new AnswersDto(x.Answer,x.IsCorrect);
                  }).ToList()
            };
        }

        public async Task<PageReturnDto<QuoteDto>> GetQuotes(QueryParams model)
        {
           var quotes=await _quotesRepository
                .Query()
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Include(x=>x.answersEntities)
                .ToListAsync();
            var count = await _quotesRepository.Query().CountAsync();

            var quoteDtos = quotes.Select(x =>
              {
                  return new QuoteDto(x.Id, x.Quote, x.answersEntities.Select(x =>
                    {
                        return new AnswersDto( x.Answer, x.IsCorrect);
                    }).ToList());
              });
            return new PageReturnDto<QuoteDto>(quoteDtos, count, model.Page, model.ItemsPerPage);
        }

        public async Task<QuoteDto> UpdateQuote(UpdateQuoteDto model)
        {
            var quote = await _quotesRepository
                .Query()
                .Where(x => x.Id == model.Id)
                .Include(x => x.answersEntities)
                .FirstOrDefaultAsync();
            if (quote == null)
            {
                throw new CustomException("user with this id does not exist", 404);
            }
            quote.answersEntities.Clear();
            var answers = new List<AnswersEntity>();
            foreach (var i in model.answers)
            {
                answers.Add(new AnswersEntity
                {
                    Answer = i.answer,
                    IsCorrect = i.IsCorrect,
                    CreatedAt = DateTime.Now,
                });
            }
            quote.answersEntities = answers;
            await _UnitOfWork.CompleteAsync();
            return new QuoteDto
            {
                Id = quote.Id,
                Quote = quote.Quote,
                Answers = model.answers
            };
        }
    }
}
