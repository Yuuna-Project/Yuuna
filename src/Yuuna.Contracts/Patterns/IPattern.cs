// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    using System;
    using System.Collections.Immutable;

    using Yuuna.Common.Linq;
    using Yuuna.Contracts.Semantics;

    /// <summary>
    /// 模式物件。用來關聯多個群組物件。
    /// </summary>
    public interface IPattern : IEquatable<IPattern>, IImmutable<IGroup>
    {
        /// <summary>
        /// 表示此模式物件所包含的群組數量。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 表示此模式物件所包含的群組(名稱)。
        /// </summary>
        IImmutableList<string> Groups { get; }

        /// <summary>
        /// 擁有者。
        /// </summary>
        Guid Owner { get; }
    }
}