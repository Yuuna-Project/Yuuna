// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Artisan.Cascade
{
    using System.Collections.Generic;

    internal interface IInternalSession : IInternalStorage, IInternalEventDataBus, IReadOnlyCollection<object>
    {
    }
}