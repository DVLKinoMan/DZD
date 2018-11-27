using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    internal sealed class Lookup<TKey, TElement> : ILookup<TKey, TElement>
    {
        private readonly Dictionary<TKey, List<TElement>> map;
        private readonly List<TKey> keys;

        internal Lookup(IEqualityComparer<TKey> comparer)
        {
            map = new Dictionary<TKey, List<TElement>>(comparer);
            keys = new List<TKey>();
        }

        internal void Add(TKey key, TElement element)
        {
            List<TElement> list;
            if (!map.TryGetValue(key, out list))
            {
                list = new List<TElement>();
                map[key] = list;
                keys.Add(key);
            }
            list.Add(element);
        }

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                List<TElement> list;

                if (!map.TryGetValue(key, out list))
                    return Enumerable.Empty<TElement>();//It will not throw exception if key doesn't exists
                return list.Select(x => x);
            }
        }

        public int Count
        {
            get
            {
                return map.Count;
            }
        }

        public bool Contains(TKey key)
        {
            return map.ContainsKey(key);
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            return keys.Select(k => new Grouping<TKey, TElement>(k, map[k])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
