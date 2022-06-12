using Infrastructure.Entities.Quote.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quiz.Dtos
{
    public record struct QuizDto(int Id,string Name);
}
