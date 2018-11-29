using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<DZD> Empty<DZD>()
        {
            //It is cached for each DZD type. two DZD types access equal references
            return EmptyHolder<DZD>.Array;
        }

        private static class EmptyHolder<T>
        {
            internal static readonly T[] Array = new T[0];
        }
    }
}
