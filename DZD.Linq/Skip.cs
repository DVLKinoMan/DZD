using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.SkipImpl(count);
        }

        public static IEnumerable<TSource> SkipImpl<TSource>(this IEnumerable<TSource> source, int count)
        {
            int i = 0;
            foreach (var s in source)
            {
                if (i >= count)
                    yield return s;
                i++;
            }

            //Jon Skeet's Version
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        if (!iterator.MoveNext())
            //        {
            //            yield break;
            //        }
            //    }
            //    while (iterator.MoveNext())
            //    {
            //        yield return iterator.Current;
            //    }
            //}
        }
    }
}
