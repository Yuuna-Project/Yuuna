// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Common.Linq
{
    using System.Collections.Immutable;

    /// <summary>
    /// 表示可轉換為不可變集合物件。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IImmutable<T>
    {
        /// <summary>
        /// 轉換成不可變清單以使用 Linq 。
        /// </summary>
        /// <returns></returns>
        IImmutableList<T> ToImmutable();
    }
}