using System.Collections.Generic;

namespace DZD.Linq
{
    public interface IGrouping<out TKey, out TElement> : IEnumerable<TElement>
    {
        TKey Key { get; }
    }
}
