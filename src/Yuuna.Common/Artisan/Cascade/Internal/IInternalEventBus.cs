// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Common.Artisan.Cascade
{
    using System;
    using System.Collections.Generic;

    internal interface IInternalEventDataBus
    {
        event Action<Queue<object>> Completed;

        void OnComplete();
    }
}