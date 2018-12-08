using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Data.Extensions;
using Data.Extensions.Transformers;
using Data.Interfaces.Repositories;
using LinqKit;

namespace Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IList<TEntity> GetFilteredList<TOrderField>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrderField>> order = null,
            bool? orderAsc = true,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = QueryGetFilteredList(filter, order, orderAsc, skip, take, includes);

            return query.AsNoTracking().ToList();
        }

        public async Task<IList<TEntity>> GetFilteredListAsync<TOrderField>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrderField>> order = null,
            bool? orderAsc = true,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = QueryGetFilteredList(filter, order, orderAsc, skip, take, includes);

            return await query.AsNoTracking().ToListAsync();
        }

        private IQueryable<TEntity> QueryGetFilteredList<TOrderField>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TOrderField>> order, bool? orderAsc, int? skip, int? take,
            Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            foreach (var navigationProperty in includes)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }

            var query = dbQuery.Where(filter).AsQueryable();


            if (order != null)
            {
                if (orderAsc.HasValue && orderAsc.Value)
                {
                    query = query.OrderBy(order).AsQueryable();
                }
                else
                {
                    query = query.OrderByDescending(order).AsQueryable();
                }
            }
            else
            {
                query = query.OrderBy(o => o.Id).AsQueryable();
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return GetSingleInternal(filter, includes).FirstOrDefault();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetSingleInternal(filter, includes).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetSingleInternal(filter, includes).FirstOrDefaultAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            foreach (var navigationProperty in includes)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }

            var query = dbQuery.AsNoTracking().Where(filter).AsQueryable();

            return query.Count();
        }

        public T Max<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> max, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            foreach (var navigationProperty in includes)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }

            var query = dbQuery.Where(filter).AsQueryable();

            var any = query.Any();
            return any ? query.Max(max) : default(T);
        }

        public void Insert(params TEntity[] items)
        {
            if (items == null)
            {
                throw new NullReferenceException("There are no items to insert");
            }

            foreach (var item in items)
            {
                _dbSet.Add(item);
            }
        }

        public virtual void Delete(params TEntity[] items)
        {
            if (items == null)
            {
                throw new NullReferenceException("There are no items to delete");
            }

            if (items.Any(x => x is IDeleted))
            {
                throw new NotSupportedException("Entity of this type only supports logical deletion");
            }

            _dbSet.RemoveRange(items);
        }

        public void Update(params TEntity[] items)
        {
            if (items == null)
            {
                throw new NullReferenceException("There are no items to update");
            }
            foreach (var item in items)
            {
                //var entity = _dbSet.FirstOrDefault(f => f.Id == item.Id);
                //if (entity != null)
                //{
                //    _context.Entry(entity).State = EntityState.Detached;
                //}

                //_dbSet.Attach(item);
                //_context.Entry(item).State = EntityState.Modified;

                _dbSet.AddOrUpdate(item);
            }
        }

        public async Task<TEntity[]> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(orderBy, predicate, includeProperties);
            return await entities.ToArrayAsync();
        }

        public async Task<PaginatedList<TEntity>> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(orderBy, predicate, includeProperties);
            var total = await entities.CountAsync();
            entities = entities.Paginate(pageIndex, pageSize);
            var list = await entities.ToListAsync();
            return list.ToPaginatedList(pageIndex, pageSize, total);
        }
        public async Task<ScrollableList<TEntity>> GetScrollableAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(orderBy, predicate, includeProperties);
            var total = await entities.CountAsync();
            entities = entities.Scrollable(skip, take);
            var list = await entities.ToListAsync();
            return list.ToScrollableList(skip, take, total);
        }

        private IQueryable<TEntity> FilterQuery(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? entities.AsExpandable().Where(predicate) : entities;
            if (orderBy != null)
            {
                entities = orderBy(entities);
            }
            return entities;
        }

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }

            return entities;
        }

        private IQueryable<TEntity> GetSingleInternal(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var dbQuery = IncludeProperties(includes);

            return dbQuery.Where(filter).AsNoTracking();
        }

        public IQueryable<TEntity> Collection => _dbSet.AsNoTracking();

        public IQueryable<TEntity> CollectionWithTracking => _dbSet;
    }
}
