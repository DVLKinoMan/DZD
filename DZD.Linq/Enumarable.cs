using System;
using System.Collections;
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

        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            // Optimization for ICollection<T> 
            ICollection<TSource> genericCollection = source as ICollection<TSource>;
            if (genericCollection != null)
            {
                return genericCollection.Count;
            }

            // Optimization for ICollection 
            ICollection nonGenericCollection = source as ICollection;
            if (nonGenericCollection != null)
            {
                return nonGenericCollection.Count;
            }

            checked
            {
                int count = 0;
                using (var iterator = source.GetEnumerator())
                {
                    while (iterator.MoveNext())
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public static int Count<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            //if the overflow happen it will throw overflowexception will be thrown
            checked
            {
                int count = 0;
                foreach (var s in source)
                    if (predicate(s))
                        count++;

                return count;
            }
        }

        public static long LongCount<TSource>(this IEnumerable<TSource> source)
        {
            long count = 0;
            foreach (var s in source)
                count++;

            return count;
        }

        public static long LongCount<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            long count = 0;
            foreach (var s in source)
                if (predicate(s))
                    count++;

            return count;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            return ConcatImpl(first, second);
        }

        private static IEnumerable<TSource> ConcatImpl<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var s in first)
                yield return s;

            foreach (var s2 in second)
                yield return s2;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> func)
        {
            foreach (var s in source)
                foreach (var s2 in func(s))
                    yield return s2;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> func)
        {
            int index = 0;
            foreach (var s in source)
                foreach (var s2 in func(s, index++))
                    yield return s2;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            int index = 0;
            foreach (var s in source)
                foreach (var s2 in collectionSelector(s, index++))
                    yield return resultSelector(s, s2);
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (var s in source)
                foreach (var s2 in collectionSelector(s))
                    yield return resultSelector(s, s2);
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            //Jon Skeet's version:
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    return iterator.MoveNext();
            //}

            return source.Count() > 0;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (var s in source)
                if (predicate(s))
                    return true;

            return false;
        }

        public static bool All<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            foreach (var s in source)
                if (!predicate(s))
                    return false;

            return true;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            //Jon Skeet's Version
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    if (iterator.MoveNext())
            //    {
            //        return iterator.Current;
            //    }
            //    throw new InvalidOperationException("Sequence was empty");
            //}

            foreach (var s in source)
                return s;

            throw new InvalidOperationException();
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (var s in source)
                if (predicate(s))
                    return s;

            throw new InvalidOperationException();
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            //Jon Skeet's Version
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    return iterator.MoveNext() ? iterator.Current : default(TSource);
            //}

            foreach (var s in source)
                return s;

            return default(TSource);
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (var s in source)
                if (predicate(s))
                    return s;

            return default(TSource);
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");
                TSource s = iterator.Current;
                if (iterator.MoveNext())
                    throw new InvalidOperationException("Sequence contained multiple elements");
                return s;
            }
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            TSource result = default(TSource);
            bool findAny = false;
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    if (findAny)
                        throw new InvalidOperationException("Sequence contained multiple matching elements");
                    result = s;
                }
            }
            if (!findAny)
                throw new InvalidOperationException("Sequence did not contain matching element");

            return result;
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    return default(TSource);
                TSource s = iterator.Current;
                if (iterator.MoveNext())
                    throw new InvalidOperationException("Sequence contained multiple elements");
                return s;
            }
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            TSource result = default(TSource);
            bool findAny = false;
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    if (findAny)
                        throw new InvalidOperationException("Sequence contained multiple matching elements");
                    result = s;
                }
            }

            return result;
        }

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

        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                return list.Count == 0 ? default(TSource) : list[list.Count - 1];
            }
            TSource last = default(TSource);
            foreach (TSource item in source)
            {
                last = item;
            }
            return last;
        }

        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            TSource last = default(TSource);
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    last = item;
                }
            }
            return last;
        }
    }
}
