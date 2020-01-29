// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Recognition.Speech
{
    using System;

    using Yuuna.Contracts.Recorders;

    internal sealed class Duration : IDisposable
    {
        private readonly Action _onStop;

        internal Duration(IRecorder recorder, Action onStop)
        {
            recorder.Start();
            this._onStop = recorder.Stop + onStop;
        }

        void IDisposable.Dispose()
        {
            this._onStop.Invoke();
        }
    }
}