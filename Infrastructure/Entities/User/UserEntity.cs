using Infrastructure.Entities.QuizResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.User
{
    [Table("Users",Schema ="dbo")]
    public class UserEntity:IPrimaryKeyEntity,IBaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<QuizResultsEntity> quizResultsEntities { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
