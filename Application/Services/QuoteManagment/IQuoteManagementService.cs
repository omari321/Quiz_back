using Infrastructure.Entities.Questions;
using Infrastructure.Entities.Questions.Dtos;
using Infrastructure.Entities.Quote.Dtos;
using Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.QuoteManagment
{
    public interface IQuoteManagementService
    {
        Task<QuoteDto> CreteQuote(CreateQuoteDto model);
        Task<QuotesEntity> DeleteQuote(int QuoteId);
        Task<QuoteDto> UpdateQuote(UpdateQuoteDto model);
        Task<PageReturnDto<QuoteDto>> GetQuotes(QueryParams model);
        Task<QuoteDto> GetQuoteById(int QuoteId);

    }
}
