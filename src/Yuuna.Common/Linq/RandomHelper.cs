// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Linq
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Yuuna.Common.Utils;

    public static class RandomHelper
    {
        private static ConcurrentDictionary<object, int> s_binarySwitcher = new ConcurrentDictionary<object, int>();
        private static ConcurrentDictionary<object, int> s_manySwitcher = new ConcurrentDictionary<object, int>();

        /// <summary>
        /// 隨機從集合中取得一個成員。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">集合。</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"/>
        public static T RandomTakeOne<T>(this IEnumerable<T> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            var list = collection.ToArray();
            if (list.Length == 0)
                return default(T);
            else if (list.Length == 1)
            {
                return list[0];
            }
            else if (list.Length == 2)
            {
                if (!s_binarySwitcher.ContainsKey(collection))
                {
                    var rnd = new Random(Guid.NewGuid().GetHashCode());
                    s_binarySwitcher.TryAdd(collection, rnd.Next(0, list.Length));
                }

                return list[(s_binarySwitcher[collection] ^= 1)];
            }
            else
            {
            GenRnd:
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                var newIndex = rnd.Next(0, list.Length);
                if (!s_manySwitcher.TryGetValue(collection, out var index))
                    s_manySwitcher.TryAdd(collection, newIndex);
                else if (index.Equals(newIndex))
                    goto GenRnd;
                else
                    s_manySwitcher[collection] = newIndex;
                return list[newIndex];
            }
        }

        public static T RandomTakeOne<T>(this IImmutable<T> collection)
        {
            return collection.ToImmutable().RandomTakeOne();
        }
    }
}