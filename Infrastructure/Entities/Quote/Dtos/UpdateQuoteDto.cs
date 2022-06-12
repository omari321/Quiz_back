using Infrastructure.Entities.Answers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quote.Dtos
{
    public record struct UpdateQuoteDto(int Id, List<AnswersDto> answers);
}
