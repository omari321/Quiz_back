using Application.Services.UserQuizResultsManagment;
using Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController:ControllerBase
    {
        private readonly IUserQuizResultsManagmentService userQuizResultsManagmentService;

        public ResultController(IUserQuizResultsManagmentService userQuizResultsManagmentService)
        {
            this.userQuizResultsManagmentService = userQuizResultsManagmentService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserResults(QueryParams model)
        {
            return Ok(await userQuizResultsManagmentService.GetUserResults(model));
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetUserResultsForUser(int userId, QueryParams model)
        {
            return Ok(await userQuizResultsManagmentService.GetUserResultsForUser(userId,model));
        }
    }
}
