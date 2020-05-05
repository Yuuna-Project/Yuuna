// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Evaluation
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;

    using Yuuna.Contracts.Patterns;
    using Yuuna.Contracts.Semantics;

    public class Match : IEquatable<Match>, IComparable<Match>
    {
        /// <summary>
        /// 是否完全符合。
        /// </summary>
        public bool IsExactMatch { get; }

        /// <summary>
        /// </summary>
        public IImmutableList<ISynonym> Matches { get; }

        /// <summary>
        /// 表示缺少的群組物件清單。
        /// </summary>
        public IImmutableList<IGroup> Missing { get; }

        /// <summary>
        /// 比較的文法。
        /// </summary>
        public IPattern Pattern { get; }

        private Match(IPattern pattern, IImmutableList<ISynonym> matches, IImmutableList<IGroup> missing, bool isExactMatch)
        {
            this.Pattern = pattern;
            this.Matches = matches;
            this.Missing = missing;
            this.IsExactMatch = isExactMatch;
        }

        /// <summary>
        /// 評估 <paramref name="pattern"/> 與 <paramref name="feed"/> 的符合程度並返回一個 <see cref="Match"/> 物件。
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="feed"></param>
        /// <returns></returns>
        public static Match CountPatternMatch(IPattern pattern, IImmutableList<string> feed)
        {
            var matches = ImmutableArray.CreateBuilder<ISynonym>();

            using (var iterator = pattern.ToImmutable().GetEnumerator())
            {
                iterator.MoveNext();
                foreach (var word in feed)
                {
                    if (iterator.Current.TryGetSynonym(word, out var s))
                    {
                        matches.Add(s);
                        if (!iterator.MoveNext())
                            break;
                    }
                }
            }
            var missing = pattern.ToImmutable().Except(matches.Select(x => x.Owner)).ToImmutableArray();

            return new Match(pattern, matches.ToImmutable(), missing, missing.Length.Equals(default));
        }

        public int CompareTo(Match other)
        {
            return this.Matches.Count - other.Matches.Count;
        }

        public bool Equals(Match other)
        {
            return this.Pattern.Equals(other.Pattern);
        }
    }
}