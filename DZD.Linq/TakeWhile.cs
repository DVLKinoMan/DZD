using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.TakeWhileImpl(predicate);
        }

        public static IEnumerable<TSource> TakeWhileImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            using (var iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext() && predicate(iterator.Current))
                {
                    yield return iterator.Current;
                }
            }
        }

        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.TakeWhileImpl(predicate);
        }

        public static IEnumerable<TSource> TakeWhileImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int count = 0;
            using (var iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext() && predicate(iterator.Current, count++))
                {
                    yield return iterator.Current;
                }
            }
        }
    }
}
