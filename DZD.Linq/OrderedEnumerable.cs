using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    internal class OrderedEnumerable<TElement, TCompositeKey> : IOrderedEnumerable<TElement>
    {
        private readonly IEnumerable<TElement> source;
        private readonly Func<TElement, TCompositeKey> compositeSelector;
        private readonly IComparer<TCompositeKey> compositeComparer;
        internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TCompositeKey> compositeSelector, IComparer<TCompositeKey> compositeComparer)
        {
            this.source = source;
            this.compositeSelector = compositeSelector;
            this.compositeComparer = compositeComparer;
        }

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            comparer = comparer ?? Comparer<TKey>.Default;
            if (descending)
            {
                comparer = new ReverseComparer<TKey>(comparer);
            }
            // Copy to a local variable so we don’t need to capture "this" 
            Func<TElement, TCompositeKey> primarySelector = compositeSelector;
            Func<TElement, CompositeKey<TCompositeKey, TKey>> newKeySelector =
                element => new CompositeKey<TCompositeKey, TKey>(primarySelector(element), keySelector(element));

            IComparer<CompositeKey<TCompositeKey, TKey>> newKeyComparer =
                new CompositeKey<TCompositeKey, TKey>.Comparer(compositeComparer, comparer);

            return new OrderedEnumerable<TElement, CompositeKey<TCompositeKey, TKey>>
                (source, newKeySelector, newKeyComparer);
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            // First copy the elements into an array: don’t bother with a list, as we 
            // want to use arrays for all the swapping around. 
            int count;
            TElement[] data = source.ToBuffer(out count);
            int[] indexes = new int[count];
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i;
            }

            TCompositeKey[] keys = new TCompositeKey[count];
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = compositeSelector(data[i]);
            }

            QuickSort(indexes, keys, 0, count - 1);

            for (int i = 0; i < indexes.Length; i++)
            {
                yield return data[indexes[i]];
            }
        }

        private void QuickSort(int[] indexes, TCompositeKey[] keys, int left, int right)
        {
            if (right > left)
            {
                int pivot = left + (right - left) / 2;
                int pivotPosition = Partition(indexes, keys, left, right, pivot);
                QuickSort(indexes, keys, left, pivotPosition - 1);
                QuickSort(indexes, keys, pivotPosition + 1, right);
            }
        }
        private int Partition(int[] indexes, TCompositeKey[] keys, int left, int right, int pivot)
        {
            // Remember the current index (into the keys/elements arrays) of the pivot location 
            int pivotIndex = indexes[pivot];
            TCompositeKey pivotKey = keys[pivotIndex];

            // Swap the pivot value to the end 
            indexes[pivot] = indexes[right];
            indexes[right] = pivotIndex;
            int storeIndex = left;
            for (int i = left; i < right; i++)
            {
                int candidateIndex = indexes[i];
                TCompositeKey candidateKey = keys[candidateIndex];
                int comparison = compositeComparer.Compare(candidateKey, pivotKey);
                if (comparison < 0 || (comparison == 0 && candidateIndex < pivotIndex))
                {
                    // Swap storeIndex with the current location 
                    indexes[i] = indexes[storeIndex];
                    indexes[storeIndex] = candidateIndex;
                    storeIndex++;
                }
            }
            // Move the pivot to its final place 
            int tmp = indexes[storeIndex];
            indexes[storeIndex] = indexes[right];
            indexes[right] = tmp;
            return storeIndex;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // Interface implementations here 
    }
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
            //List<TElement> elements = source.ToList();
            //while (elements.Count > 0)
            //{
            //    TElement minElement = elements[0];
            //    int minIndex = 0;
            //    for (int i = 1; i < elements.Count; i++)
            //    {
            //        if (currentComparer.Compare(elements[i], minElement) < 0)
            //        {
            //            minElement = elements[i];
            //            minIndex = i;
            //        }
            //    }
            //    elements.RemoveAt(minIndex);
            //    yield return minElement;
            //}

            // First copy the elements into an array: don’t bother with a list, as we 
            // want to use arrays for all the swapping around. 
            int count;
            TElement[] data = source.ToBuffer(out count);
            TElement[] tmp = new TElement[count];

            MergeSort(data, tmp, 0, count - 1);
            for (int i = 0; i < count; i++)
            {
                yield return data[i];
            }
        }

        private void MergeSort(TElement[] data, TElement[] tmp, int left, int right)
        {
            if (right > left)
            {
                if (right == left + 1)
                {
                    TElement leftElement = data[left];
                    TElement rightElement = data[right];
                    if (currentComparer.Compare(leftElement, rightElement) > 0)
                    {
                        data[left] = rightElement;
                        data[right] = leftElement;
                    }
                }
                else
                {
                    int mid = left + (right - left) / 2;
                    MergeSort(data, tmp, left, mid);
                    MergeSort(data, tmp, mid + 1, right);
                    Merge(data, tmp, left, mid + 1, right);
                }
            }
        }

        private void Merge(TElement[] data, TElement[] tmp, int left, int mid, int right)
        {
            int leftCursor = left;
            int rightCursor = mid;
            int tmpCursor = left;
            TElement leftElement = data[leftCursor];
            TElement rightElement = data[rightCursor];
            // By never merging empty lists, we know we’ll always have valid starting points 
            while (true)
            {
                // When equal, use the left element to achieve stability 
                if (currentComparer.Compare(leftElement, rightElement) <= 0)
                {
                    tmp[tmpCursor++] = leftElement;
                    leftCursor++;
                    if (leftCursor < mid)
                    {
                        leftElement = data[leftCursor];
                    }
                    else
                    {
                        // Only the right list is still active. Therefore tmpCursor must equal rightCursor, 
                        // so there’s no point in copying the right list to tmp and back again. Just copy 
                        // the already-sorted bits back into data. 
                        Array.Copy(tmp, left, data, left, tmpCursor - left);
                        return;
                    }
                }
                else
                {
                    tmp[tmpCursor++] = rightElement;
                    rightCursor++;
                    if (rightCursor <= right)
                    {
                        rightElement = data[rightCursor];
                    }
                    else
                    {
                        // Only the left list is still active. Therefore we can copy the remainder of 
                        // the left list directly to the appropriate place in data, and then copy the 
                        // appropriate portion of tmp back. 
                        Array.Copy(data, leftCursor, data, tmpCursor, mid - leftCursor);
                        Array.Copy(tmp, left, data, left, tmpCursor - left);
                        return;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
