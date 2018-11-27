using System.Collections.Generic;

namespace DZD.Linq
{
    internal sealed class NullKeyFriendlyDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> map;
        private bool haveNullKey = false;
        private TValue valueForNullKey;

        internal NullKeyFriendlyDictionary(IEqualityComparer<TKey> comparer)
        {
            map = new Dictionary<TKey, TValue>(comparer);
        }

        internal bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                // This will be default(TValue) if haveNullKey is false,
                // which is what we want.
                value = valueForNullKey;
                return haveNullKey;
            }
            return map.TryGetValue(key, out value);
        }

        internal TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    if (haveNullKey)
                    {
                        return valueForNullKey;
                    }
                    throw new KeyNotFoundException("No null key");
                }
                return map[key];
            }
            set
            {
                if (key == null)
                {
                    haveNullKey = true;
                    valueForNullKey = value;
                }
                else
                {
                    map[key] = value;
                }
            }
        }

        internal int Count
        {
            get { return map.Count + (haveNullKey ? 1 : 0); }
        }

        internal bool ContainsKey(TKey key)
        {
            return key == null ? haveNullKey : map.ContainsKey(key);
        }
    }
}
