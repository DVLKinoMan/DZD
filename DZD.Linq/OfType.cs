using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    partial class Enumerable
    {
        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (default(TResult) != null)
            {
                IEnumerable<TResult> existingSequence = source as IEnumerable<TResult>;
                if (existingSequence != null)
                {
                    return existingSequence;
                }
            }
            return OfTypeImpl<TResult>(source);
        }

        private static IEnumerable<TResult> OfTypeImpl<TResult>(IEnumerable source)
        {
            foreach (object item in source)
            {
                if (item is TResult)
                {
                    yield return (TResult)item;
                }
            }
        }
    }
}
