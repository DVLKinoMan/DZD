using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            //Following doesn't buffers Second sequence
            //foreach (var o in outer)
            //    foreach (var i in inner)
            //        if (comparer.Equals(outerKeySelector(o), innerKeySelector(i)))
            //            yield return resultSelector(o, i);

            var lookup = inner.ToLookup(innerKeySelector, comparer);//imediate execution
            var selectMany = outer.SelectMany(outerElement => lookup[outerKeySelector(outerElement)], resultSelector);//deffered

            foreach (var s in selectMany)
                yield return s;
        }
    }
}
