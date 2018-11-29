using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TDZD> Where<TDZD>(this IEnumerable<TDZD> list, Func<TDZD, bool> func)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (func == null)
                throw new ArgumentNullException("func");

            return WhereImpl(list, func);
        }

        private static IEnumerable<TDZD> WhereImpl<TDZD>(IEnumerable<TDZD> list, Func<TDZD, bool> func)
        {
            foreach (var l in list)
                if (func(l))
                    yield return l;
        }

        public static IEnumerable<TDZD> Where<TDZD>(this IEnumerable<TDZD> list, Func<TDZD, int, bool> func)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (func == null)
                throw new ArgumentNullException("func");

            return WhereImpl(list, func);
        }

        private static IEnumerable<TDZD> WhereImpl<TDZD>(IEnumerable<TDZD> list, Func<TDZD, int, bool> func)
        {
            var count = 0;
            foreach (var l in list)
            {
                if (func(l, count))
                    yield return l;
                count++;
            }
        }
    }
}
