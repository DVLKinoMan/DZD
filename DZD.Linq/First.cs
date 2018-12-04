using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            //Jon Skeet's Version
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    if (iterator.MoveNext())
            //    {
            //        return iterator.Current;
            //    }
            //    throw new InvalidOperationException("Sequence was empty");
            //}

            foreach (var s in source)
                return s;

            throw new InvalidOperationException();
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (var s in source)
                if (predicate(s))
                    return s;

            throw new InvalidOperationException();
        }
    }
}
