using Infrastructure.Entities.Answers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Questions.Dtos
{
    public record struct  CreateQuoteDto(string Quote,List<AnswersDto> answers);
}
