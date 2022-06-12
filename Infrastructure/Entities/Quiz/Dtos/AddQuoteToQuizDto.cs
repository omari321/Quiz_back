using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quiz.Dtos
{
    public record struct AddQuoteToQuizDto(int QuizId, List<int> QuoteIds);
}
