using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static double Average(this IEnumerable<int> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            checked
            {
                long count = 0;
                long total = 0;
                foreach (int item in source)
                {
                    total += item;
                    count++;
                }

                if (count == 0)
                    throw new InvalidOperationException("Sequence was empty");

                return (double)total / (double)count;
            }
        }

        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select(selector).Average();
        }

        public static double? Average(this IEnumerable<int?> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            checked
            {
                long count = 0;
                long total = 0;
                foreach (int? item in source)
                {
                    if (item != null)
                    {
                        count++;
                        total += item.Value;
                    }
                }
                return count == 0 ? (double?)null : (double)total / (double)count;
            }
        }

        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return source.Select(selector).Average();
        }
    }
}
