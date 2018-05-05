using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions.Transformers
{
    public class ScrollableList<T>
    {
        public int ToTake { get; set; }
        public int ToSkip { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }


        public ScrollableList()
        {

            Items = new List<T>();
        }

        public ScrollableList(IEnumerable<T> source, int skip, int take, int totalCount)
        {

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Items = source.ToList();
            ToTake = take;
            ToSkip = skip;
            TotalCount = totalCount;
        }
    }
}
