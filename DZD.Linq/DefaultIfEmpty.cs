using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty(default(TSource));
        }

        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.DefaultIfEmptyImpl(defaultValue);
        }

        private static IEnumerable<TSource> DefaultIfEmptyImpl<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            bool foundAny = false;
            foreach (var s in source)
            {
                foundAny = true;
                yield return s;
            }

            if (!foundAny)
                yield return defaultValue;
        }
    }
}
