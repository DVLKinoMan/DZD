using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TRes> Repeat<TRes>(TRes res, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            return RepeatImpl(res, count);
        }

        private static IEnumerable<TRes> RepeatImpl<TRes>(TRes res, int count)
        {
            for (int i = 0; i < count; i++)
                yield return res;
        }
    }
}
