using System;
using System.Collections;
using System.Collections.Generic;

namespace DZD.Linq
{
    partial class Enumerable
    {
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            if(source==null)
                throw new ArgumentNullException("source");
            IEnumerable<TResult> existingSequence = source as IEnumerable<TResult>;
            if (existingSequence != null)
            {
                return existingSequence;
            }
            return CastImpl<TResult>(source);
        }

        private static IEnumerable<TResult> CastImpl<TResult>(IEnumerable source)
        {
            foreach (object item in source)
            {
                yield return (TResult)item;
            }
        }
    }
}
