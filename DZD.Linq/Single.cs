using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");
                TSource s = iterator.Current;
                if (iterator.MoveNext())
                    throw new InvalidOperationException("Sequence contained multiple elements");
                return s;
            }
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            TSource result = default(TSource);
            bool findAny = false;
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    if (findAny)
                        throw new InvalidOperationException("Sequence contained multiple matching elements");
                    result = s;
                }
            }
            if (!findAny)
                throw new InvalidOperationException("Sequence did not contain matching element");

            return result;
        }
    }
}
