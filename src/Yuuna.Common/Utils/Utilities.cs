// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Utils
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode]
    public static class Utilities
    {
        /// <summary>
        /// 當 <paramref name="obj"/> 為 <see langword="null"/> 時， 引發 <see
        /// cref="ArgumentNullException"/> 例外。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">物件。</param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentNullException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ThrowIfNull<T>(this T obj, string paramName) where T : class
        {
            if (obj is null)
                throw new ArgumentNullException(paramName);
            return obj;
        }

        /// <summary>
        /// 當 <paramref name="str"/> 滿足 <see cref="string.IsNullOrEmpty"/> 的條件時， 引發 <see
        /// cref="ArgumentNullException"/> 例外。
        /// </summary>
        /// <param name="str">字串。</param>
        /// <param name="paramName">參數名稱。</param>
        /// <exception cref="ArgumentNullException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ThrowIfNullOrEmpty(this string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException($"'{paramName}' is null or empty.", paramName);
            return str;
        }
    }
}