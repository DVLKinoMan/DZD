using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            //Jon Skeet's version
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count == 0)
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                return list[list.Count - 1];
            }

            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                TSource last = iterator.Current;
                while (iterator.MoveNext())
                {
                    last = iterator.Current;
                }
                return last;
            }

            //DVL Version
            //TSource result = default(TSource);
            //bool foundAny = false;
            //foreach (var s in source)
            //{
            //    result = s;
            //    foundAny = true;
            //}

            //if (!foundAny)
            //    throw new InvalidOperationException("Sequence was empty");

            //return result;
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            //Testing list do not include in Jon Skeet's blog because LINQ to Object doesn't contain it
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count == 0)
                {
                    throw new InvalidOperationException("Sequence was empty");
                }

                int i = 1;
                while (i != list.Count - 1)
                    if (predicate(list[list.Count - i]))
                        return list[list.Count - i];

                throw new InvalidOperationException("Sequence does not contains matching elements");
            }

            TSource result = default(TSource);
            bool foundAny = false;
            foreach (var s in source)
                if (predicate(s))
                {
                    result = s;
                    foundAny = true;
                }

            if (!foundAny)
                throw new InvalidOperationException("Sequence does not contains matching elements");

            return result;
        }
    }
}
