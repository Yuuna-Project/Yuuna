// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

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