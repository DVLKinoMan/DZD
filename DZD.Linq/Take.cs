using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.TakeImpl(count);
        }

        public static IEnumerable<TSource> TakeImpl<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count < 0)
                count = 0;

            int i = 0;
            foreach (var s in source)
            {
                if (i == count)
                    break;
                yield return s;
                i++;
            }

            //Jon Skeet's Version
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    for (int i = 0; i < count && iterator.MoveNext(); i++)
            //    {
            //        yield return iterator.Current;
            //    }
            //}
        }
    }
}
