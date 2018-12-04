using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            foreach (var s in source)
                if (!predicate(s))
                    return false;

            return true;
        }
    }
}
