// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Evaluation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using Yuuna.Contracts.Patterns;

    public class DefaultStrategy : IStrategy
    {
        public Alternative FindBest(IEnumerable<IRule> patternSetCollection, IImmutableList<string> feed)
        {
            var list = ImmutableArray.CreateBuilder<Match>();
            foreach (var pattern in patternSetCollection)
            {
                foreach (var p in pattern.ToImmutable())
                {
                    var match = Match.CountPatternMatch(p, feed);
                    list.Add(match);
                }
            }

            var e = this.SelectBestMatch(list.ToImmutable());
            return e;
        }

        /// <summary>
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        protected virtual Alternative SelectBestMatch(IImmutableList<Match> matches)
        {
            //if(matches is null || matches.Count == 0)
            //     return //沒有安裝任何模組

            var sta = new SortedList<int, List<Match>>();
            foreach (var match in matches)
            {
                var key = match.Matches.Count;

                if (!sta.TryGetValue(key, out var list))
                {
                    list = new List<Match>();
                    sta.Add(key, list);
                }
                list.Add(match);
            }

            var weight = double.MinValue;
            var bestSelect = default(List<Match>);
            foreach (var s in sta)
            {
                var y = this.Quantize(s.Value.Count, matches.Count, s.Key);
                if (y > weight)
                {
                    weight = y;
                    bestSelect = s.Value;
                }
            }

            return Alternative.Create(bestSelect?.ToImmutableArray());
        }

        /// <summary>
        /// 量化
        /// </summary>
        /// <param name="elementCount"></param>
        /// <param name="categorizedCount"></param>
        /// <param name="matchedCount"></param>
        /// <returns></returns>
        private double Quantize(int elementCount, int categorizedCount, int matchedCount)
        {
            return Math.Tanh(Math.Pow(Math.E, (elementCount) / (double)-(categorizedCount * matchedCount)));
        }
    }
}