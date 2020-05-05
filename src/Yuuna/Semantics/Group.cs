// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Semantics
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics;

    using Yuuna.Common.Utils;
    using Yuuna.Contracts.Semantics;

    /// <summary>
    /// 群體物件。用來存放相同性質類型的同義詞物件。
    /// </summary>
    internal sealed class Group : IGroup
    {
        private readonly ImmutableArray<ISynonym>.Builder _synonyms;

        /// <summary>
        /// 群組名稱。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 建立新的群體物件實體。
        /// </summary>
        /// <param name="key">名稱</param>
        internal Group(string key)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(key), "'groupName' can't be null or empty.");
            this.Name = key;
            this._synonyms = ImmutableArray.CreateBuilder<ISynonym>();
        }

        /// <summary>
        /// 嘗試使用交集取得同義詞物件。 若具有交集則將參數中的值附加至該搜尋到的同義詞物件中；若無，則透過參數建立新的同義詞實體並傳回。
        /// </summary>
        /// <param name="words">欲關聯至此物件的多個單字。若此群組物件不包含任何與此參數相關的單字，則使用此參數建立新同義詞實體。</param>
        /// <param name="stringComparer">字串比對器。</param>
        /// <exception cref="ArgumentNullException"/>
        public ISynonym AppendOrCreate(IEnumerable<string> words, StringComparer stringComparer = null)
        {
            words.ThrowIfNull(nameof(words));

            foreach (var synonym in this._synonyms)
            {
                if (synonym.Equals(words))
                {
                    synonym.AddRange(words);
                    return synonym;
                }
            }

            var inst = new Synonym(this, stringComparer);
            inst.AddRange(words);
            this._synonyms.Add(inst);
            return inst;
        }

        /// <summary>
        /// 比對兩個 <see cref="IGroup"/> 物件的群組名稱是否相等。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IGroup other)
        {
            return this.Name.Equals(other.Name);
        }

        /// <summary>
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Equals(string word)
        {
            return this.TryGetSynonym(word, out _);
        }

        public IImmutableList<ISynonym> ToImmutable()
        {
            return this._synonyms.ToImmutable();
        }

        public bool TryGetSynonym(string word, out ISynonym synonyms)
        {
            foreach (var synonym in this._synonyms)
            {
                if (synonym.Equals(word))
                {
                    synonyms = synonym;
                    return true;
                }
            }
            synonyms = null;
            return false;
        }
    }
}