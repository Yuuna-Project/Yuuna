// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Optimization
{
    using System.Collections.Immutable;
    using System.Linq;

    public class Alternative
    {
        public AlternativeStatus Status { get; }
        public IImmutableList<Match> Matches { get; }

        public Alternative(IImmutableList<Match> matches)
        {
            var count = matches?.Count ?? 0;
            if (count.Equals(1))
            {
                var single = matches[0].Missing.Count;
                if (single.Equals(0))
                {
                    this.Status = AlternativeStatus.Optimal;  // 最佳解

                }
                else if (single.Equals(1))
                {
                    this.Status = AlternativeStatus.Condition;// 近似解(缺一組條件，詢問使用者) 
                }
            }
            else if (count.Equals(2))
            {
                if (matches.All(x => x.Missing.Count == 0))
                {
                    this.Status = AlternativeStatus.Paradox;// 內部錯誤(多個意圖重複)
                }
                else if (matches.All(x => x.Missing.Count == 1))
                {
                    this.Status = AlternativeStatus.Proposition;// 兩個近似解，詢問使用者(二選一 + 離開情境)
                }
            } 
            //else if (count.Equals(0))
            //{
            //    // 無有效模組
            //    this.Status = AlternativeStatus.NoModuleInstalled;
            //}
            else
            {
                // 不採用
                this.Status = AlternativeStatus.Invalid;
            }

            this.Matches = matches;
        }
         
    }
}