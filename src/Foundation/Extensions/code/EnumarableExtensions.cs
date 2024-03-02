using System;
using System.Collections.Generic;

namespace DreamTeam.Foundation.Extensions
{
    public static class EnumarableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, TKey targetKeyValue)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(targetKeyValue))
                {
                    yield return element;
                }
            }
        }
    }
}