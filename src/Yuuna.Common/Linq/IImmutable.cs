// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

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
        /// 轉換成不可變清單。
        /// </summary>
        /// <returns></returns>
        IImmutableList<T> ToImmutable();
    }
}