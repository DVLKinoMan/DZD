﻿using System.Collections.Generic;

namespace DZD.Linq
{
    internal class CompoundComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> primary;
        private readonly IComparer<T> secondary;
        internal CompoundComparer(IComparer<T> primary, IComparer<T> secondary)
        {
            this.primary = primary;
            this.secondary = secondary;
        }

        public int Compare(T x, T y)
        {
            int primaryResult = primary.Compare(x, y);
            if (primaryResult != 0)
            {
                return primaryResult;
            }
            return secondary.Compare(x, y);
        }
    }
}
