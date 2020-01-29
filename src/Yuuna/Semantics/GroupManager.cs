// Author: Orlys
// Github: https://github.com/Orlys
namespace Yuuna.Semantics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    using Yuuna.Contracts.Semantics;

    internal sealed class GroupManager : IGroupManager
    {
        private readonly Dictionary<string, IGroup> _groups;
        private readonly object _lock = new object();

        /// <summary>
        /// 群組名稱集合。
        /// </summary>
        public IReadOnlyCollection<string> Keys
        {
            get
            {
                lock (this._lock)
                {
                    return this._groups.Keys;
                }
            }
        }

        /// <summary>
        /// 透過群組名稱取得群組物件；若無法取得則回傳 <see langword="null"/>。
        /// </summary>
        /// <param name="key">群組名稱。</param>
        /// <returns></returns>
        public IGroup this[string key]
        {
            get
            {
                this.TryGetGroup(key, out var g);
                return g;
            }
        }

        internal GroupManager()
        {
            this._groups = new Dictionary<string, IGroup>();
        }

        /// <summary>
        /// 建立新的群組物件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IGroup Define(string key)
        {
            ValidateGroupKey(key);

            lock (this._lock)
            {
                var g = new Group(key);
                this._groups.Add(g.Key, g);
                return g;
            }
        }

        /// <summary>
        /// 嘗試透過群組名稱取得群組物件。
        /// </summary>
        /// <param name="key">群組名稱。</param>
        /// <param name="group">群組物件。</param>
        /// <returns></returns>
        public bool TryGetGroup(string key, out IGroup group)
        {
            ValidateGroupKey(key);
            lock (this._lock)
            {
                return this._groups.TryGetValue(key, out group);
            }
        }

        [DebuggerNonUserCode]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateGroupKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
        }
    }
}