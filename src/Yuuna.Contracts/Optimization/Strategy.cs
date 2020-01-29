// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Optimization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    using Yuuna.Contracts.Patterns;
    using Yuuna.Contracts.Modules;
    using Yuuna.Contracts.Semantics;
    using System.Diagnostics;

    public class Strategy : IStrategy
    {
        public Strategy()
        {

        }

        public Alternative FindBest(IEnumerable<IPatternSet> patternSetCollection, IImmutableList<string> feed)
        {
            var list = ImmutableArray.CreateBuilder<Match>();
            foreach (var pattern in patternSetCollection)
            {
                foreach (var p in pattern.ToImmutable())
                {
                    var match = this.CountPatternMatch(p, feed);  
                    list.Add(match);
                }
            }

            var e = this.SelectBestMatch(list.ToImmutable());
            return e;
        }

        /// <summary>
        /// 
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
                var key = match.SequentialMatches.Count;
                if (!sta.ContainsKey(key))
                { 
                    sta.Add(key, new List<Match> { match }); 
                }
                else
                {
                    sta[key].Add(match);
                }
            }

            sta.Remove(0); //過濾掉完全沒相符的資料以免影響結果


            var weight = double.MinValue;
            var bestSelect = default(List<Match>);
            foreach (var s in sta)
            {
                //var y =  Math.Tanh(Math.Pow(Math.E, (s.Value.Count) / (double)-(matches.Count *  s.Key)));
               var y = Quantize(s.Value.Count, matches.Count, s.Key);
                Debug.WriteLine((s.Key, y));
                if (y > weight)
                {
                    weight = y;
                    bestSelect = s.Value;
                }
            }

            var a = new Alternative(bestSelect?.ToImmutableArray()); 
            return a;
        }

        private static double Quantize(int elementCount, int categorizedCount, int matchedCount)
        {
            return Math.Tanh(Math.Pow(Math.E, (elementCount) / (double)-(categorizedCount * matchedCount)));
        }

        /// <summary>
        /// 評估 <paramref name="pattern"/> 與 <paramref name="feed"/> 的符合程度並返回一個 <see cref="Match"/> 物件。
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="feed"></param>
        /// <returns></returns>
        protected virtual Match CountPatternMatch(IPattern pattern, IImmutableList<string> feed)
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

            return new Match(pattern, matches.ToImmutable());
        }
    }
}