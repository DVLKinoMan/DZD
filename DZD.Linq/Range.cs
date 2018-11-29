using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            //This is also long and do not need casts
            //if(start + 1L + count)
            if (start + (long)count - 1L > int.MaxValue)
                throw new ArgumentOutOfRangeException("count");

            return RangeImpl(start, count);
        }

        private static IEnumerable<int> RangeImpl(int start, int count)
        {
            for (int temp = start; temp < start + count; temp++)
                yield return temp;
        }
    }
}
