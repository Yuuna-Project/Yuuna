// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Utils
{
    using System;

    using Yuuna.Contracts.Patterns;

    public static class InvokeExtensions
    {
        public static void InvokeWith(this Invoke invoke, IInvokeBuilder first, params IInvokeBuilder[] rest)
        {
            if (invoke is null)
                throw new ArgumentNullException(nameof(invoke));
            if (first is null)
                throw new ArgumentNullException(nameof(first));
            if (rest is null)
                throw new ArgumentNullException(nameof(rest));

            first.OnInvoke(invoke);
            foreach (var ib in rest)
            {
                ib.OnInvoke(invoke);
            }
        }
    }
}