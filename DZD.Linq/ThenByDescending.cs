﻿using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ThenByDescending(keySelector, Comparer<TKey>.Default);
        }

        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            IComparer<TSource> sourceComparer = new ProjectionComparer<TSource, TKey>(keySelector, comparer);
            sourceComparer = new ReverseComparer<TSource>(sourceComparer);

            return new OrderedEnumerable<TSource>(source, sourceComparer);
        }
    }
}
