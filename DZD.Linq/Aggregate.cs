using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (func == null)
                throw new ArgumentNullException("func");

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");

                TSource previous = iterator.Current;
                while (iterator.MoveNext())
                    previous = func(previous, iterator.Current);

                return previous;
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (seed == null)
                throw new ArgumentNullException("seed");

            if (func == null)
                throw new ArgumentNullException("func");

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");

                TAccumulate current = func(seed, iterator.Current);
                while (iterator.MoveNext())
                    current = func(current, iterator.Current);

                return current;
            }
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (seed == null)
                throw new ArgumentNullException("seed");

            if (func == null)
                throw new ArgumentNullException("func");

            if (resultSelector == null)
                throw new ArgumentNullException("resultSelector");

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");

                TAccumulate current = func(seed, iterator.Current);
                while (iterator.MoveNext())
                    current = func(current, iterator.Current);

                return resultSelector(current);
            }
        }
    }
}
