using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DZD.Linq
{
    internal class ReverseComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> forwardComparer;
        internal ReverseComparer(IComparer<T> forwardComparer)
        {
            this.forwardComparer = forwardComparer;
        }

        public int Compare(T x, T y)
        {
            return forwardComparer.Compare(y, x);
        }
    }
}
