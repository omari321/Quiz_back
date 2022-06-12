using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryRelated
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity, IPrimaryKeyEntity
    {
        protected Db_Context _entityDbContext;
        public GenericRepository(Db_Context db)
        {
            _entityDbContext = db;
        }
        public async Task AddAsync(T entity)
        {
            await _entityDbContext.Set<T>().AddAsync(entity);
        }

        public async Task<bool> CheckIfMeetsConditionAsync(Expression<Func<T, bool>> condition)
        {
            return await _entityDbContext.Set<T>().AnyAsync(condition);
        }
        public async Task<T> FindById(T entity)
        {
            return await _entityDbContext.Set<T>().Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
        }
        public async Task<List<T>> FindManyByConditionAsync(Expression<Func<T, bool>> condition)
        {
            return await _entityDbContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> condition)
        {
            return await _entityDbContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public IQueryable<T> Query()
        {
            return _entityDbContext.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            _entityDbContext.Set<T>().Remove(entity);
        }
    }
}
