// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

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