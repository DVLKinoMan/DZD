using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
        {
            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            //My Implementation
            //foreach (var o in outer)
            //{
            //    var list = new List<TInner>();
            //    var list2 = new List<TOuter> { o };
            //    yield return resultSelector(o, list2.Join(inner, outerKeySelector, innerKeySelector, (out1, iner1) => iner1, comparer));
            //}

            //My Second Implementation
            var lookup = inner.ToLookup(innerKeySelector, comparer);

            foreach (var o in outer)
            {
                var key = outerKeySelector(o);
                yield return resultSelector(o, lookup[key]);
            }
        }
    }
}
