// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Common.Artisan.Cascade
{
    internal interface IInternalStorage
    {
        void Store<T>(T data);
    }
}