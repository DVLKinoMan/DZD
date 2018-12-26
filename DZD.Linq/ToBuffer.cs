using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        internal static TSource[] ToBuffer<TSource>(this IEnumerable<TSource> source, out int count)
        {
            // Optimize for ICollection<T> 
            ICollection<TSource> collection = source as ICollection<TSource>;
            if (collection != null)
            {
                count = collection.Count;
                TSource[] tmp = new TSource[count];
                collection.CopyTo(tmp, 0);
                return tmp;
            }
            // We’ll have to loop through, creating and copying arrays as we go 
            TSource[] ret = new TSource[16];
            int tmpCount = 0;
            foreach (TSource item in source)
            {
                // Need to expand… 
                if (tmpCount == ret.Length)
                {
                    Array.Resize(ref ret, ret.Length * 2);
                }
                ret[tmpCount++] = item;
            }
            count = tmpCount;
            return ret;
        }
    }
}
