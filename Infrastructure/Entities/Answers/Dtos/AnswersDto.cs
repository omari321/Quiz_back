using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Answers.Dtos
{
    public record AnswersDto(string answer,bool IsCorrect);
}
