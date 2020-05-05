// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Optimization.Utils
{
    public static class Utilities
    {
        /// <summary>
        /// 在 <paramref name="collection"/> 中順序性的尋找 <paramref name="compare"/> 中的元素， 並返回所有相符的元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="compare"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        //public static IEnumerable<T> SequenceMatchCount<T, T2>(this IPatternSet collection,
        //    IEnumerable<T2> compare, Func<T, T2, bool> comparer)
        //{
        //    collection.ThrowIfNull(nameof(collection));
        //    compare.ThrowIfNull(nameof(compare));
        //    comparer.ThrowIfNull(nameof(comparer));

        // if (collection.Count() < compare.Count()) yield break;

        //    var c = compare.GetEnumerator();
        //    if (!c.MoveNext())
        //    {
        //        yield break;
        //    }
        //    foreach (var item in collection)
        //    {
        //        if (comparer(item, c.Current))
        //        {
        //            yield return item;
        //            if (!c.MoveNext())
        //                break;
        //        }
        //    }
        //}
    }
}