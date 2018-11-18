using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    public static partial class Enumarable
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
