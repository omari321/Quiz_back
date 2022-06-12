using Application.Services.QuoteManagment;
using Infrastructure.Entities.Questions.Dtos;
using Infrastructure.Entities.Quote.Dtos;
using Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController:ControllerBase
    {
        private readonly IQuoteManagementService _quoteManagementService;

        public QuotesController(IQuoteManagementService quoteManagementService)
        {
            _quoteManagementService = quoteManagementService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreteQuote(CreateQuoteDto model)
        {
            return Ok(await _quoteManagementService.CreteQuote(model));
        }
        [HttpPost("[action]/{QuoteId}")]
        public async Task<IActionResult> DeleteQuote(int QuoteId)
        {
            return Ok(await _quoteManagementService.DeleteQuote(QuoteId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateQuote(UpdateQuoteDto model)
        {
            return Ok(await _quoteManagementService.UpdateQuote(model));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuotes([FromQuery]QueryParams model)
        {
            return Ok(await _quoteManagementService.GetQuotes(model));
        }

        [HttpGet("[action]/{QuoteId}")]
        public async Task<IActionResult> GetQuoteById(int QuoteId)
        {
            return Ok(await _quoteManagementService.GetQuoteById(QuoteId));
        }

    }
}
