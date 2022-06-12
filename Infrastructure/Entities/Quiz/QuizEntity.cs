using Infrastructure.Entities.QuizQuotes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quiz
{
    [Table("Quiz", Schema = "dbo")]
    public class QuizEntity : IBaseEntity, IPrimaryKeyEntity
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public List<QuizQuotesEntity> QuizQuotes { get; set; }
        public DateTime CreatedAt { get; set ; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
