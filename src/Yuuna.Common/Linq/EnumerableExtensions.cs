// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yuuna.Common.Utils;

    public static class EnumerableExtensions
    {
        /// <summary>
        /// 在 <paramref name="collection"/> 中順序性的尋找 <paramref name="compare"/> 中的元素， 並返回所有相符元素的總和。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static int SequenceMatchCount<T>(this IEnumerable<T> collection,
            IEnumerable<T> compare)
        {
            collection.ThrowIfNull(nameof(collection));
            collection.ThrowIfNull(nameof(compare));

            int matchCount = 0;
            if (collection.Count() < compare.Count())
                return matchCount;

            var c = compare.GetEnumerator();
            if (!c.MoveNext())
            {
                return matchCount;
            }
            foreach (var item in collection)
            {
                if (c.Current.Equals(item))
                {
                    ++matchCount;
                    if (!c.MoveNext())
                        break;
                }
            }

            return matchCount;
        }

        /// <summary>
        /// 在 <paramref name="collection"/> 中順序性的尋找 <paramref name="compare"/> 中的元素， 並返回所有相符的元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="compare"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> SequenceMatchCount<T, T2>(this IEnumerable<T> collection,
            IEnumerable<T2> compare, Func<T, T2, bool> comparer)
        {
            collection.ThrowIfNull(nameof(collection));
            compare.ThrowIfNull(nameof(compare));
            comparer.ThrowIfNull(nameof(comparer));

            if (collection.Count() < compare.Count())
                yield break;

            var c = compare.GetEnumerator();
            if (!c.MoveNext())
            {
                yield break;
            }
            foreach (var item in collection)
            {
                if (comparer(item, c.Current))
                {
                    yield return item;
                    if (!c.MoveNext())
                        break;
                }
            }
        }
    }
}