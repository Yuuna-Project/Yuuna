// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Common.Artisan.Cascade
{
    using System.Collections.Generic;

    internal interface IInternalSession : IInternalStorage, IInternalEventDataBus, IReadOnlyCollection<object>
    {
    }
}