// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Evaluation
{
    public enum AlternativeStatus
    {
        /// <summary>
        /// 無有效解。
        /// </summary>
        Invalid,

        /// <summary>
        /// 最佳解。表示與模式 (Pattern) 完全符合。
        /// </summary>
        Optimal,

        /// <summary>
        /// 條件式。表示缺少一個群組 (Group)。
        /// </summary>
        Condition,

        /// <summary>
        /// 命題。表示具有兩個條件式 ( <see cref="Condition"/>)。
        /// </summary>
        Proposition,

        /// <summary>
        /// 悖論。表示具有兩個(含)以上的最佳解 ( <see cref="Optimal"/>)，這實際上屬於模組設計上的問題。
        /// </summary>
        Paradox,

        /// <summary>
        /// 沒有已安裝的模組。
        /// </summary>
        NoModuleInstalled = -1,
    }
}