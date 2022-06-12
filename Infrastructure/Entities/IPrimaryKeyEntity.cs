using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public interface IPrimaryKeyEntity
    {
        int Id { get; set; }
    }
}
