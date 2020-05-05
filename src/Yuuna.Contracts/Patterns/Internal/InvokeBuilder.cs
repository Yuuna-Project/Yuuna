// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    using System;

    using Yuuna.Common.Artisan.Cascade;

    //internal sealed class InvokeBuilder : Pipeline<IncompleteBuilder, Invoke>, IInvokeBuilder
    //{
    //    public IIncompleteBuilder OnInvoke(Invoke invoke)
    //    {
    //        if (invoke is null)
    //            throw new ArgumentNullException(nameof(invoke));
    //        return this.MoveNext(invoke);
    //    }
    //}

    internal sealed class InvokeBuilder : Output<Invoke>, IInvokeBuilder
    {
        public void OnInvoke(Invoke invoke)
        {
            if (invoke is null)
                throw new ArgumentNullException(nameof(invoke));
            this.MoveNext(invoke);
            //return this.MoveNext(invoke);
        }
    }
}