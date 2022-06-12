using Infrastructure.Entities.Answers;
using Infrastructure.Entities.Quiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Questions
{
    [Table("Quotes", Schema = "dbo")]
    public class QuotesEntity : IBaseEntity, IPrimaryKeyEntity
    {
        public int Id { set; get; }
        public string Quote { set; get; }
        public List<AnswersEntity> answersEntities { set; get; }
        public DateTime CreatedAt { set; get; }
        public DateTime? UpdatedAt { set; get; }
        public bool? IsDisabled { get; set; }
    }
}
