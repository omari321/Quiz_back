using Infrastructure.Entities.Answers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quote.Dtos
{
    public record struct QuoteDto(int Id,string Quote,List<AnswersDto> Answers);
}
