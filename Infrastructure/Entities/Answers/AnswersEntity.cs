using Infrastructure.Entities.Questions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Answers
{
    [Table("Answers", Schema = "dbo")]
    public class AnswersEntity : IBaseEntity, IPrimaryKeyEntity
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int QuoteId { get; set; }
        public QuotesEntity Quote { get; set; }
        public bool IsCorrect { get; set; }
        public bool? IsDisabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
