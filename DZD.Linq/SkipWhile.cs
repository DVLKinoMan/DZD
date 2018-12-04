using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.SkipWhileImpl(predicate);
        }

        public static IEnumerable<TSource> SkipWhileImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            //My Version - I think this is not working correctly
            //using (var iterator = source.GetEnumerator())
            //{
            //    while (iterator.MoveNext() && predicate(iterator.Current))
            //        ;

            //    yield return iterator.Current;

            //    while (iterator.MoveNext())
            //    {
            //        yield return iterator.Current;
            //    }
            //}

            //Jon Skeet's Version
            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    TSource item = iterator.Current;
                    if (!predicate(item))
                    {
                        // Stop skipping now, and yield this item 
                        yield return item;
                        break;
                    }
                }
                while (iterator.MoveNext())
                {
                    yield return iterator.Current;
                }
            }
        }

        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.SkipWhileImpl(predicate);
        }

        public static IEnumerable<TSource> SkipWhileImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            //My Version - I think this is not working correctly
            //int count = 0;
            //using (var iterator = source.GetEnumerator())
            //{
            //    while (iterator.MoveNext() && predicate(iterator.Current, count))
            //        count++;

            //    yield return iterator.Current;

            //    while (iterator.MoveNext())
            //    {
            //        yield return iterator.Current;
            //    }
            //}

            //Jon Skeet's Version
            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                int index = 0;
                while (iterator.MoveNext())
                {
                    TSource item = iterator.Current;
                    if (!predicate(item, index))
                    {
                        // Stop skipping now, and yield this item 
                        yield return item;
                        break;
                    }
                    index++;
                }
                while (iterator.MoveNext())
                {
                    yield return iterator.Current;
                }
            }
        }
    }
}
