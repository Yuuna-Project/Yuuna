// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Patterns
{
    using System;
    using System.Collections.Immutable;

    using Yuuna.Contracts.Semantics;
    using Yuuna.Common.Linq;
    using Yuuna.Contracts.Modules;

    public interface IPattern : IEquatable<IPattern>, IImmutable<IGroup>
    {
        Guid Owner { get; }

        int Count { get; }

        IImmutableList<string> SequentialKeys { get; }
    }
}