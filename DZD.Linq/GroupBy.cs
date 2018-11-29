using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            //argument validations
            var lookUp = source.ToLookup(keySelector);

            foreach (var l in lookUp)
                yield return l;
        }

        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            //argument validations
            var lookUp = source.ToLookup(keySelector, comparer);

            foreach (var l in lookUp)
                yield return l;
        }


        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            //argument validations
            var lookUp = source.ToLookup(keySelector, elementSelector);

            foreach (var l in lookUp)
                yield return l;
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            //argument validations
            var lookUp = source.ToLookup(keySelector, elementSelector, comparer);

            foreach (var l in lookUp)
                yield return l;
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return source.GroupBy(keySelector).Select(gr => resultSelector(gr.Key, gr));
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return source.GroupBy(keySelector, comparer).Select(gr => resultSelector(gr.Key, gr));
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return source.GroupBy(keySelector, elementSelector).Select(gr => resultSelector(gr.Key, gr));
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return source.GroupBy(keySelector, elementSelector, comparer).Select(gr => resultSelector(gr.Key, gr));
        }
    }
}
