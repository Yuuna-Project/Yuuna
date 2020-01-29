// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Semantics
{
    using System;
    using System.Collections.Generic;

    using Yuuna.Common.Linq;

    /// <summary>
    /// 同義詞物件
    /// </summary>
    public interface ISynonym : IEquatable<IEnumerable<string>>, IEquatable<string>, IEquatable<ISynonym>, IImmutable<string>
    {
        /// <summary>
        /// 表示該物件隸屬的群體。
        /// </summary>
        IGroup Owner { get; }

        /// <summary>
        /// 字串比較器。
        /// </summary>
        StringComparer StringComparer { get; }

        /// <summary>
        /// 加入單個單字。
        /// </summary>
        /// <param name="word">單字。</param>
        /// <returns></returns>
        bool Add(string word);

        /// <summary>
        /// 加入多個單字。
        /// </summary>
        /// <param name="words">單字列表。</param>
        /// <returns></returns>
        void AddRange(IEnumerable<string> words);
    }
}