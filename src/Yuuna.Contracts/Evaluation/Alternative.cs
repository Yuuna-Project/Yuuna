// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Evaluation
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// 替代方案。
    /// </summary>
    public readonly struct Alternative
    {
        /// <summary>
        /// </summary>
        public readonly IImmutableList<Match> Matches;

        /// <summary>
        /// 狀態。
        /// </summary>
        public readonly AlternativeStatus Status;

        public Alternative(IImmutableList<Match> matches, AlternativeStatus status)
        {
            this.Matches = matches;
            this.Status = status;
        }

        public static Alternative Create(IEnumerable<Match> matches)
        {
            var m = default(IImmutableList<Match>);
            if (matches is null)
                m = ImmutableArray<Match>.Empty;
            else
            {
                m = matches as IImmutableList<Match>;
                if (m is null)
                    m = matches.ToImmutableArray();
            }

            var status = AlternativeStatus.Invalid;
            switch (m.Count)
            {
                case 1:
                    {
                        const uint n = 2;
                        var single = m[0].Missing.Count;
                        if (single < n)
                        {
                            status = AlternativeStatus.Optimal;  // 最佳解
                        }
                        else if (single == n)
                        {
                            status = AlternativeStatus.Condition;// 近似解(缺 n 組條件，詢問使用者)
                        }
                        break;
                    }
                case 2:
                    {
                        if (m.All(x => x.Missing.Count == 0))
                        {
                            status = AlternativeStatus.Paradox;// 內部錯誤(多個意圖重複)
                        }
                        else if (m.All(x => x.Missing.Count == 1))
                        {
                            status = AlternativeStatus.Proposition;// 兩個近似解，詢問使用者(二選一 + 離開情境)
                        }
                        break;
                    }
                case 0:
                    {
                        // 無有效模組
                        status = AlternativeStatus.NoModuleInstalled;
                        break;
                    }
                default:
                    // 不採用
                    break;
            }
            return new Alternative(m, status);
        }
    }
}