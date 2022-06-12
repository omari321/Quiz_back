using Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Quiz.Dtos
{
    public class QuizParameters: QueryParams
    {
        public string SearchTerm { get; set; } = "";
    }
}
