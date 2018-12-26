using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            //Just copyed Jon Skeet's Version 1st version
            //if (source == null)
            //{
            //    throw new ArgumentNullException("source");
            //}

            //// Optimize for ICollection<T> 
            //ICollection<TSource> collection = source as ICollection<TSource>;
            //if (collection != null)
            //{
            //    TSource[] tmp = new TSource[collection.Count];
            //    collection.CopyTo(tmp, 0);
            //    return tmp;
            //}

            //// We’ll have to loop through, creating and copying arrays as we go 
            //TSource[] ret = new TSource[16];
            //int count = 0;
            //foreach (TSource item in source)
            //{
            //    if (count == ret.Length)
            //    {
            //        Array.Resize(ref ret, ret.Length * 2);
            //    }
            //    ret[count++] = item;
            //}

            //// Now create another copy if we have to, in order to get an array of the 
            //// right size 
            //if (count != ret.Length)
            //{
            //    Array.Resize(ref ret, count);
            //}

            //return ret;

            //Just copyed Jon Skeet's Version second version
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            int count;
            TSource[] ret = source.ToBuffer(out count);
            // Now create another copy if we have to, in order to get an array of the 
            // right size 
            if (count != ret.Length)
            {
                Array.Resize(ref ret, count);
            }
            return ret;
        }
    }
}
