// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Semantics
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Linq;

    using Yuuna.Contracts.Semantics;

    internal sealed class Synonym : ISynonym
    {
        private readonly static StringComparer DefaultComparer = StringComparer.CurrentCultureIgnoreCase;

        private readonly HashSet<string> _set;

        public IGroup Owner { get; }

        public StringComparer StringComparer { get; }

        internal Synonym(Group owner, StringComparer stringComparer)
        {
            this.StringComparer = stringComparer is null ? DefaultComparer : stringComparer;
            this._set = new HashSet<string>(this.StringComparer);
            Debug.Assert(owner != null, "'owner' can't be null");
            this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public bool Add(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            // todo: word 可能需要其他檢查 檢查是否全為 CJK

            return this._set.Add(word.Trim());
        }

        public void AddRange(IEnumerable<string> words)
        {
            if (words is null)
                return;
            foreach (var s in words)
                this.Add(s);
        }

        public bool Equals(IEnumerable<string> manyStrings)
        {
            if (manyStrings is null)
                return false;
            foreach (var str in manyStrings)
            {
                if (this.Equals(str))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 比較該同義詞物件是否與單字等價。這個比較方法會搜尋集合中是否包含該單字。
        /// </summary>
        /// <param name="word">單字。</param>
        /// <returns></returns>
        public bool Equals(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            return this._set.Contains(word, this.StringComparer);
        }

        public bool Equals(ISynonym synonym)
        {
            if (synonym is null)
                return false;
            return this.Owner.Equals(synonym.Owner);
        }

        public IImmutableList<string> ToImmutable()
        {
            return this._set.ToImmutableArray();
        }
    }
}