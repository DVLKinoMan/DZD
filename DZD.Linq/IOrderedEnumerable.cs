﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    public interface IOrderedEnumerable<TElement> : IEnumerable<TElement>, IEnumerable
    {
        IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
    }
}
