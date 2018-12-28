﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
        {
            TSource ret;
            if (!TryElementAt(source, index, out ret))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return ret;
        }
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            TSource ret;
            // We don’t care about the return value – ret will be default(TSource) if it’s false 
            TryElementAt(source, index, out ret);
            return ret;
        }

        private static bool TryElementAt<TSource>(IEnumerable<TSource> source, int index, out TSource element)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            element = default(TSource);
            if (index < 0)
            {
                return false;
            }
            ICollection<TSource> collection = source as ICollection<TSource>;
            if (collection != null)
            {
                int count = collection.Count;
                if (index >= count)
                {
                    return false;
                }
                // If it’s a list, we know we’re okay now – just return directly… 
                IList<TSource> list = source as IList<TSource>;
                if (list != null)
                {
                    element = list[index];
                    return true;
                }
            }
            ICollection nonGenericCollection = source as ICollection;
            if (nonGenericCollection != null)
            {
                int count = nonGenericCollection.Count;
                if (index >= count)
                {
                    return false;
                }
            }
            // We don’t need to fetch the current value each time – get to the right 
            // place first. 
            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                // Note use of -1 so that we start off my moving onto element 0. 
                // Don’t want to use i <= index in case index == int.MaxValue! 
                for (int i = -1; i < index; i++)
                {
                    if (!iterator.MoveNext())
                    {
                        return false;
                    }
                }
                element = iterator.Current;
                return true;
            }
        }
    }
}