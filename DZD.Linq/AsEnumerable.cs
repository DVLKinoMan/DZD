using System.Collections.Generic;

namespace DZD.Linq
{
    partial class Enumerable
    {
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return source;
        }
    }
}
