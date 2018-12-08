using Contracts;
using Data.Extensions.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Data.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IList<TEntity> GetFilteredList<TOrderField>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrderField>> order = null,
            bool? orderAsc = true,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<IList<TEntity>> GetFilteredListAsync<TOrderField>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrderField>> order = null,
            bool? orderAsc = true,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes);

        TEntity GetSingle(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity[]> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<PaginatedList<TEntity>> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<ScrollableList<TEntity>> GetScrollableAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties);

        int Count(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes);

        T Max<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> max,
            params Expression<Func<TEntity, object>>[] includes);

        void Insert(params TEntity[] items);

        void Delete(params TEntity[] items);

        void Update(params TEntity[] items);

        IQueryable<TEntity> Collection { get; }

        IQueryable<TEntity> CollectionWithTracking { get; }
    }
}
