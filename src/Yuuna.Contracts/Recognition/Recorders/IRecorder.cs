// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Recorders
{
    using System;
    using System.IO;

    public interface IRecorder
    {
        event Action<Stream> Completed;

        void Start();

        void Stop();
    }
} 