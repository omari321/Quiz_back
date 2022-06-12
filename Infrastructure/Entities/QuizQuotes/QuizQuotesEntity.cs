using Infrastructure.Entities.Questions;
using Infrastructure.Entities.Quiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.QuizQuotes
{
    [Table("QuizQuotes", Schema = "dbo")]
    public class QuizQuotesEntity : IBaseEntity, IPrimaryKeyEntity
    {
        public int Id { get; set; } 
        public int QuizId { get; set; }
        public QuizEntity Quiz { get; set; }
        public int QuoteId { get; set; }
        public QuotesEntity Quote { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
