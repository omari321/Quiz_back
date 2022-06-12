using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryRelated
{
    public interface IGenericRepository<T>
        where T : class, IBaseEntity, IPrimaryKeyEntity 
    {
        Task<bool> CheckIfMeetsConditionAsync(Expression<Func<T, bool>> condition);
        IQueryable<T> Query();
        Task<List<T>> FindManyByConditionAsync(Expression<Func<T, bool>> condition);
        Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> condition);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task<T> FindById(T entity);
    }
}
