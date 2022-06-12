using Infrastructure.Entities.QuizResults.Dtos;
using Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserQuizResultsManagment
{
    public interface IUserQuizResultsManagmentService
    {
        Task<PageReturnDto<QuizResultDto>> GetUserResults(QueryParams model);
        Task<PageReturnDto<QuizResultDto>> GetUserResultsForUser(int userId,QueryParams model);
    }
}
