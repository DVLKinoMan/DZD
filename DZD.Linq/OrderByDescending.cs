using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderByDescending(keySelector, Comparer<TKey>.Default);
        }

        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            IComparer<TSource> sourceComparer = new ProjectionComparer<TSource, TKey>(keySelector, comparer);
            sourceComparer = new ReverseComparer<TSource>(sourceComparer);

            return new OrderedEnumerable<TSource>(source, sourceComparer);
        }
    }
}
