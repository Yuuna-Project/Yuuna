// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Semantics
{
    using System.Collections.Generic;

    /// <summary>
    /// 群組物件管理器
    /// </summary>
    public interface IGroupManager
    {
        /// <summary>
        /// 群組名稱集合。
        /// </summary>
        IReadOnlyCollection<string> Keys { get; }

        /// <summary>
        /// 透過群組名稱取得群組物件；若無法取得則回傳 <see langword="null"/>。
        /// </summary>
        /// <param name="key">群組名稱。</param>
        /// <returns></returns>
        IGroup this[string key] { get; }

        /// <summary>
        /// 建立新的群組物件。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IGroup Define(string key);

        /// <summary>
        /// 嘗試透過群組名稱取得群組物件。
        /// </summary>
        /// <param name="key">群組名稱。</param>
        /// <param name="group">群組物件。</param>
        /// <returns></returns>
        bool TryGetGroup(string key, out IGroup group);
    }
}