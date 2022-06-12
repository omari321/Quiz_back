using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorkRelated
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly Db_Context _entityDbContext;
        public UnitOfWork(Db_Context entityDbContext)
        {
            _entityDbContext = entityDbContext;
        }
        public async Task CompleteAsync()
        {
            await _entityDbContext.SaveChangesAsync();
        }
    }
}
