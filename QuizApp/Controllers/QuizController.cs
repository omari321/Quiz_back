using Application.Services;
using Infrastructure.Entities.Quiz.Dtos;
using Infrastructure.Enums;
using Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController:ControllerBase
    {

        private readonly IQuizManagmentService _quizManagmentService;

        public QuizController(IQuizManagmentService quizManagmentService)
        {
            _quizManagmentService = quizManagmentService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateQuiz(CreateQuizDto model)
        {
            return Ok(await _quizManagmentService.CreateQuiz(model));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddQuotesToQuiz(AddQuoteToQuizDto model)
        {
            return Ok(await _quizManagmentService.AddQuotesToQuiz(model));
        }
        [HttpPost("[action]/{QuizId}")]
        public async Task<IActionResult> DisableQuiz(int QuizId)
        {
            return Ok(await _quizManagmentService.DisableQuiz(QuizId));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuizes([FromQuery]QuizParameters model)
        {
            return Ok(await _quizManagmentService.GetQuizes(model));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDisabledQuizes([FromQuery]QueryParams model)
        {
            return Ok(await _quizManagmentService.GetDisabledQuizes(model));
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> LoadQuizQuotes(int userId,int QuizId, QuizType quizType)
        {
            return Ok(await _quizManagmentService.LoadQuizQuotes(userId,QuizId,quizType));
        }
        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> SubmitQuiz(int userId, QuizSubmitDto model)
        {
            return Ok(await _quizManagmentService.SubmitQuiz(userId, model));
        }

    }
}
