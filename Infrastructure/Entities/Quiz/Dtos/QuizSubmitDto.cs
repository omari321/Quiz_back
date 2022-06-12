using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quiz.Dtos
{
    public record QuizSubmitDto(int quizId,QuizType quizType,List<bool> answers);
}
