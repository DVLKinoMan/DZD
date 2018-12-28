using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static int Sum(this IEnumerable<int> source)
        {
            return Sum(source, x => x);
        }
        public static int? Sum(this IEnumerable<int?> source)
        {
            return Sum(source, x => x);
        }

        public static int Sum<T>(this IEnumerable<T> source, Func<T, int> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            checked
            {
                int sum = 0;
                foreach (T item in source)
                {
                    sum += selector(item);
                }
                return sum;
            }
        }

        public static int? Sum<T>(this IEnumerable<T> source, Func<T, int?> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            checked
            {
                int sum = 0;
                foreach (T item in source)
                {
                    sum += selector(item).GetValueOrDefault();
                }
                return sum;
            }
        }
    }
}
