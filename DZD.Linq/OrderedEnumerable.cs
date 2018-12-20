using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    internal class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        private readonly IEnumerable<TElement> source;
        private readonly IComparer<TElement> currentComparer;
        internal OrderedEnumerable(IEnumerable<TElement> source,
            IComparer<TElement> comparer)
        {
            this.source = source;
            this.currentComparer = comparer;
        }

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>
            (Func<TElement, TKey> keySelector,
             IComparer<TKey> comparer,
             bool descending)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            IComparer<TElement> secondaryComparer =
                new ProjectionComparer<TElement, TKey>(keySelector, comparer);
            if (descending)
            {
                secondaryComparer = new ReverseComparer<TElement>(secondaryComparer);
            }
            return new OrderedEnumerable<TElement>(source,
                new CompoundComparer<TElement>(currentComparer, secondaryComparer));
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            List<TElement> elements = source.ToList();
            while (elements.Count > 0)
            {
                TElement minElement = elements[0];
                int minIndex = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    if (currentComparer.Compare(elements[i], minElement) < 0)
                    {
                        minElement = elements[i];
                        minIndex = i;
                    }
                }
                elements.RemoveAt(minIndex);
                yield return minElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
