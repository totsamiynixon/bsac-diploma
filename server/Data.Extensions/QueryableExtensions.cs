using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0) pageIndex = 1;
            var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return entities;
        }
        public static IQueryable<T> Scrollable<T>(this IQueryable<T> query, int skip, int take)
        {
            var entities = query.Skip(skip).Take(take);
            return entities;
        }
    }
}
