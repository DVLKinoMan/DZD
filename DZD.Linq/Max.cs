using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static int Max(this IEnumerable<int> source)
        {
            return PrimitiveMax(source);
        }
        public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            // Select will validate the arguments 
            return PrimitiveMax(source.Select(selector));
        }

        public static int? Max(this IEnumerable<int?> source)
        {
            return NullablePrimitiveMax(source);
        }

        public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            // Select will validate the arguments 
            return NullablePrimitiveMax(source.Select(selector));
        }

        // These are uses by all the overloads which use a known numeric type. 
        // The term "primitive" isn’t truly accurate here as decimal is not a primitive 
        // type, but it captures the aim reasonably well. 
        // The constraint of being a value type isn’t really required, because we don’t rely on 
        // it within the method and only code which already knows it’s a comparable value type 
        // will call these methods anyway. 

        private static T PrimitiveMax<T>(IEnumerable<T> source) where T : struct, IComparable<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            using (IEnumerator<T> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                T max = iterator.Current;
                while (iterator.MoveNext())
                {
                    T item = iterator.Current;
                    if (max.CompareTo(item) < 0)
                    {
                        max = item;
                    }
                }
                return max;
            }
        }
        private static T? NullablePrimitiveMax<T>(IEnumerable<T?> source) where T : struct, IComparable<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            T? max = null;
            foreach (T? item in source)
            {
                if (item != null &&
                    (max == null || max.Value.CompareTo(item.Value) < 0))
                {
                    max = item;
                }
            }
            return max;
        }

        public static TSource Max<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            // This condition will be true for reference types and nullable value types, and false for
            // non-nullable value types. 
            return default(TSource) == null ? NullableGenericMax(source) : NonNullableGenericMax(source);
        }
        public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary> 
        /// Implements the generic behaviour for non-nullable value types. 
        /// </summary> 
        /// <remarks> 
        /// Empty sequences will cause an InvalidOperationException to be thrown. 
        /// Note that there’s no *compile-time* validation in the caller that the type 
        /// is a non-nullable value type, hence the lack of a constraint on T. 
        /// </remarks> 
        private static T NonNullableGenericMax<T>(IEnumerable<T> source)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            using (IEnumerator<T> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                T max = iterator.Current;
                while (iterator.MoveNext())
                {
                    T item = iterator.Current;
                    if (comparer.Compare(max, item) < 0)
                    {
                        max = item;
                    }
                }
                return max;
            }
        }

        /// <summary> 
        /// Implements the generic behaviour for nullable types – both reference types and nullable 
        /// value types. 
        /// </summary> 
        /// <remarks> 
        /// Empty sequences and sequences comprising only of null values will cause the null value 
        /// to be returned. Any sequence containing non-null values will return a non-null value. 
        /// </remarks> 
        private static T NullableGenericMax<T>(IEnumerable<T> source)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            T max = default(T);
            foreach (T item in source)
            {
                if (item != null &&
                    (max == null || comparer.Compare(max, item) < 0))
                {
                    max = item;
                }
            }
            return max;
        }
    }
}
