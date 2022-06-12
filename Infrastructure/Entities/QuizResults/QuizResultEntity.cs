using Infrastructure.Entities.Quiz;
using Infrastructure.Entities.User;
using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.QuizResults
{
    [Table("QuizResult", Schema = "dbo")]
    public class QuizResultsEntity : IBaseEntity, IPrimaryKeyEntity
    {
        public int Id { set; get; }
        public int UserId { set; get; }
        public UserEntity User { set; get; }
        public int QuizId { set; get; }
        public QuizEntity Quiz { set; get; }
        public QuizType QuizType { set; get; }
        public int CorrectAnswers { set; get; }
        public int TotalQuestions { set; get; }
        public DateTime CreatedAt { set; get; }
        public DateTime? UpdatedAt { set; get; }
        public bool? IsDisabled { get; set; }
    }
}
