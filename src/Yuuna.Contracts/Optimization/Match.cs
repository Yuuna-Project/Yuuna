// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Optimization
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;

    using Yuuna.Contracts.Patterns;
    using Yuuna.Contracts.Semantics;

    public struct Match : IEquatable<Match>, IComparable<Match>
    {
        /// <summary>
        /// 比較的文法。
        /// </summary>
        public IPattern Pattern { get; }

        /// <summary>
        /// 表示缺少的群組物件清單。
        /// </summary>
        public IImmutableList<IGroup> Missing { get; }

        /// <summary> 
        /// </summary>
        public IImmutableList<ISynonym> SequentialMatches { get; }

        /// <summary>
        /// 是否完全符合。
        /// </summary>
        public bool IsExactMatch { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern">比對的目標</param>
        /// <param name="matches">從頭逐一比對後符合的項目</param>
        public Match(IPattern pattern, IImmutableList<ISynonym> matches)
        {
            this.Pattern = pattern;
            this.SequentialMatches = matches;
            this.Missing = pattern.ToImmutable().Except(matches.Select(x => x.Owner)).ToImmutableArray();
            this.IsExactMatch = this.Missing.Count.Equals(default);
        }

        public bool Equals(Match other)
        {
            return this.Pattern.Equals(other.Pattern);
        }

        public int CompareTo(Match other)
        {
            return this.SequentialMatches.Count - other.SequentialMatches.Count;
        }
    } 
}