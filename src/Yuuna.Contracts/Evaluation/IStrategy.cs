// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Evaluation
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using Yuuna.Contracts.Patterns;

    /// <summary>
    /// 策略物件。用以評估 的互動回應。
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// 找出最佳解。
        /// </summary>
        /// <param name="rules">多個規則。</param>
        /// <param name="feed">已分詞的詞集。</param>
        /// <returns></returns>
        Alternative FindBest(IEnumerable<IRule> rules, IImmutableList<string> feed);
    }
}