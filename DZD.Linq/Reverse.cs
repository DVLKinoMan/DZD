using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source is null");
            return ReverseImpl(source);
        }

        private static IEnumerable<TSource> ReverseImpl<TSource>(IEnumerable<TSource> source)
        {
            int count;
            TSource[] array = source.ToBuffer(out count);
            for (int i = count - 1; i >= 0; i--)
            {
                yield return array[i];
            }
        }
    }
}
