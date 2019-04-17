using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    partial class Enumerable
    {
        public static bool SequenceEqual<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }

            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            int count1;
            int count2;
            if (TryFastCount(first, out count1) && TryFastCount(second, out count2))
            {
                if (count1 != count2)
                {
                    return false;
                }
            }

            comparer = comparer ?? EqualityComparer<TSource>.Default;

            using (IEnumerator<TSource> iterator1 = first.GetEnumerator(),
                iterator2 = second.GetEnumerator())
            {
                while (true)
                {
                    bool next1 = iterator1.MoveNext();
                    bool next2 = iterator2.MoveNext();
                    // Sequences aren’t of same length. We don’t 
                    // care which way round. 
                    if (next1 != next2)
                    {
                        return false;
                    }

                    // Both sequences have finished – done 
                    if (!next1)
                    {
                        return true;
                    }

                    if (!comparer.Equals(iterator1.Current, iterator2.Current))
                    {
                        return false;
                    }
                }
            }
        }

        private static bool TryFastCount<TSource>(
            IEnumerable<TSource> source,
            out int count)
        {
            // Optimization for ICollection<T> 
            ICollection<TSource> genericCollection = source as ICollection<TSource>;
            if (genericCollection != null)
            {
                count = genericCollection.Count;
                return true;
            }
            // Optimization for ICollection 
            ICollection nonGenericCollection = source as ICollection;
            if (nonGenericCollection != null)
            {
                count = nonGenericCollection.Count;
                return true;
            }
            // Can’t retrieve the count quickly. Oh well. 
            count = 0;
            return false;
        }
    }
}
