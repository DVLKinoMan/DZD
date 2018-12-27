using System.Collections.Generic;

namespace DZD.Linq
{
    internal struct CompositeKey<TPrimary, TSecondary>
    {
        private readonly TPrimary primary;
        private readonly TSecondary secondary;
        internal TPrimary Primary { get { return primary; } }
        internal TSecondary Secondary { get { return secondary; } }

        internal CompositeKey(TPrimary primary, TSecondary secondary)
        {
            this.primary = primary;
            this.secondary = secondary;
        }

        internal sealed class Comparer : IComparer<CompositeKey<TPrimary, TSecondary>>
        {
            private readonly IComparer<TPrimary> primaryComparer;
            private readonly IComparer<TSecondary> secondaryComparer;

            internal Comparer(IComparer<TPrimary> primaryComparer,
                              IComparer<TSecondary> secondaryComparer)
            {
                this.primaryComparer = primaryComparer;
                this.secondaryComparer = secondaryComparer;
            }

            public int Compare(CompositeKey<TPrimary, TSecondary> x,
                               CompositeKey<TPrimary, TSecondary> y)
            {
                int primaryResult = primaryComparer.Compare(x.Primary, y.Primary);
                if (primaryResult != 0)
                {
                    return primaryResult;
                }
                return secondaryComparer.Compare(x.Secondary, y.Secondary);
            }
        }
    }
}
