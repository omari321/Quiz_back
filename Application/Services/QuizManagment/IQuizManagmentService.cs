using Infrastructure.Entities.Quiz.Dtos;
using Infrastructure.Entities.QuizQuotes;
using Infrastructure.Entities.Quote.Dtos;
using Infrastructure.Entities.User.Dtos;
using Infrastructure.Enums;
using Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IQuizManagmentService
    {
      Task<QuizDto> CreateQuiz(CreateQuizDto model);
      Task<List<QuizQuotesEntity>> AddQuotesToQuiz(AddQuoteToQuizDto model);
      Task<bool> DisableQuiz(int QuizId);
      Task<PageReturnDto<QuizDto>> GetQuizes(QuizParameters model);
      Task<PageReturnDto<QuizDto>> GetDisabledQuizes(QueryParams model);
      Task<IEnumerable<QuoteDto>> LoadQuizQuotes(int userId, int QuizId,QuizType quizType);
        Task<bool> SubmitQuiz(int userId, QuizSubmitDto model);
    }
}
