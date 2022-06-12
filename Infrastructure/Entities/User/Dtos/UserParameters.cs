using Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.User.Dtos
{
    public class UserParameters:QueryParams
    {
        public string SearchTerm { get; set; } = "";
    }
}
