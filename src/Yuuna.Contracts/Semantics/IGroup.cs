// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Semantics
{
    using System;
    using System.Collections.Generic;

    using Yuuna.Common.Linq;

    /// <summary>
    /// 群體物件。用來存放相同性質類型的同義詞物件。
    /// </summary>
    public interface IGroup : IEquatable<IGroup>, IImmutable<ISynonym>, IEquatable<string>
    {
        /// <summary>
        /// 群組名稱。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 嘗試使用交集取得同義詞物件。 若具有交集則將參數中的值附加至該搜尋到的同義詞物件中；若無，則透過參數建立新的同義詞實體並傳回。
        /// </summary>
        /// <param name="synonyms">待加入的同義詞清單。若此同義詞物件不包含任何與此清單相關的詞，則使用此參數建立新同義詞實體。</param>
        /// <param name="stringComparer">字串比對器。</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"/>
        ISynonym AppendOrCreate(IEnumerable<string> synonyms, StringComparer stringComparer = null);

        /// <summary>
        /// 嘗試取得同義詞物件。
        /// </summary>
        /// <param name="word">單詞。</param>
        /// <param name="synonyms">同義詞。</param>
        /// <returns></returns>
        bool TryGetSynonym(string word, out ISynonym synonyms);
    }
}