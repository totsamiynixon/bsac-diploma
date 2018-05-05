using Data.Extensions.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class EnumerableExtensions
    {

        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> enumerable, int pageIndex, int pageSize, int total)
        {
            return new PaginatedList<T>(enumerable, pageIndex, pageSize, total);
        }
        public static ScrollableList<T> ToScrollableList<T>(this IEnumerable<T> enumerable, int skip, int take, int total)
        {
            return new ScrollableList<T>(enumerable, skip, take, total);
        }
    }
}
