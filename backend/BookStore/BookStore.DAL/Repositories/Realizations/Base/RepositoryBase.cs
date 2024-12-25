using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookStore.DAL.Repositories.Interfaces;

namespace BookStore.DAL.Repositories.Realizations.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>, IBookStoreDbContextProvider
        where T : class
    {
        BookStoreDbContext _db;

        public BookStoreDbContext BookStoreDb { get => _db; init => _db = value; }

        protected RepositoryBase()
        {
            
        }

        protected RepositoryBase(BookStoreDbContext db)
        {
            _db = db;
        }
       
        public void Create(params T[] entities)
        {
            foreach (var e in entities)
            {
                _db.Set<T>().Add(e);
            }
        }
       
        public void Delete(params T[] entities)
        {
            foreach (var e in entities)
            { 
                _db.Set<T>().Remove(e);
            }
        }

        public void Edit(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await Task.FromResult(GetQueryable(predicate, include));
        }

        public async Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await Task.FromResult(GetQueryable(predicate, include, selector));
        }
        
        public async Task<T?> GetSingleOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).SingleOrDefaultAsync();
        }
        
        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).FirstOrDefaultAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, T>> selector,
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include, selector).FirstOrDefaultAsync();
        }

        public async Task<IQueryable<TDbSet>> GetQueryableSet<TDbSet>
            (Expression<Func<TDbSet, bool>>? predicate,
        Func<IQueryable<TDbSet>, IIncludableQueryable<TDbSet, object>>? include,
        Expression<Func<TDbSet, TDbSet>>? selector)
            where TDbSet : class
        {
            return await Task.FromResult(GetQueryable<TDbSet>(predicate, include, selector));
        }

        private IQueryable<TDbSet> GetQueryable<TDbSet>(
        Expression<Func<TDbSet, bool>>? predicate = default,
        Func<IQueryable<TDbSet>, IIncludableQueryable<TDbSet, object>>? include = default,
        Expression<Func<TDbSet, TDbSet>>? selector = default)
            where TDbSet : class
        {
            var query = _db.Set<TDbSet>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (selector is not null)
            {
                query = query.Select(selector);
            }

            return query.AsNoTracking();
        }

        private IQueryable<T> GetQueryable(
        Expression<Func<T, bool>>? predicate = default,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
        Expression<Func<T, T>>? selector = default)
        {
            var query = _db.Set<T>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (selector is not null)
            {
                query = query.Select(selector);
            }

            return query.AsNoTracking();
        }
    }
}
