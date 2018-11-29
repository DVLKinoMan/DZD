using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TRes> Select<TDZD, TRes>(this IEnumerable<TDZD> list, Func<TDZD, TRes> func)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (func == null)
                throw new ArgumentNullException("func");

            return SelectImpl(list, func);
        }

        private static IEnumerable<TRes> SelectImpl<TDZD, TRes>(IEnumerable<TDZD> list, Func<TDZD, TRes> func)
        {
            foreach (var l in list)
                yield return func(l);
        }

        public static IEnumerable<TRes> Select<TDZD, TRes>(this IEnumerable<TDZD> list, Func<TDZD, int, TRes> func)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (func == null)
                throw new ArgumentNullException("func");

            return SelectImpl(list, func);
        }

        private static IEnumerable<TRes> SelectImpl<TDZD, TRes>(IEnumerable<TDZD> list, Func<TDZD, int, TRes> func)
        {
            int count = 0;
            foreach (var l in list)
            {
                yield return func(l, count);
                count++;
            }
        }
    }
}
