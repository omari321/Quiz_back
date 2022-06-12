using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.QuizResults.Dtos
{
    public record QuizResultDto(int UserId,string UserName,int QuizId,string QuizName,int CorrectAnswers,int TotalQuestions,DateTime date);
}
