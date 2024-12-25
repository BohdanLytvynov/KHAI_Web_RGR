using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {        
        void Create(params T [] entities);
        
        void Delete(params T [] entities);

        void Edit(T entity);

        Task<IQueryable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<T?> GetSingleOrDefaultAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);

        Task<IQueryable<TDbSet>> GetQueryableSet<TDbSet>(Expression<Func<TDbSet, bool>>? predicate = default,
        Func<IQueryable<TDbSet>, IIncludableQueryable<TDbSet, object>>? include = default,
        Expression<Func<TDbSet, TDbSet>>? selector = default)
            where TDbSet : class;
    }
}
