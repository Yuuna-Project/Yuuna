// Author: Orlys
// Github: https://github.com/Orlys
namespace Yuuna.Contracts.TextSegmention
{
    using Semantics;

    using System.Collections.Immutable;
    using System.Globalization;

    /// <summary>
    /// 文字分詞器。
    /// </summary>
    public interface ITextSegmenter
    {
        /// <summary>
        /// 文化資訊，用以表示此分詞器適用的語言，詳細請看 <see cref="https://tools.ietf.org/html/bcp47"/> 。
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// 名稱。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 分詞。
        /// </summary>
        /// <param name="text">要切分的文字。</param>
        /// <returns></returns>
        IImmutableList<string> Cut(string text);

        /// <summary>
        /// 從 <paramref name="manager"/> 載入字典。
        /// </summary>
        /// <param name="manager">群組管理器。</param>
        void Load(IGroupManager manager);


        /// <summary>
        /// 從 <paramref name="manager"/> 移除字典。
        /// </summary>
        /// <param name="manager">群組管理器。</param>
        void Unload(IGroupManager manager);
    }
}