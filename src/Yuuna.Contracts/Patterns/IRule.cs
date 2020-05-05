// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    using Yuuna.Common.Linq;

    /// <summary>
    /// 規則物件。表示為多個模式物件的集合。
    /// </summary>
    public interface IRule : IImmutable<IPattern>
    {
        /// <summary>
        /// 透過 <paramref name="pattern"/> 嘗試取得 <paramref name="bag"/> 物件。
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="bag"></param>
        /// <returns></returns>
        bool TryGet(IPattern pattern, out Bag bag);
    }
}