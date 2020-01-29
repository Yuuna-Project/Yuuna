// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Recognition.Speech
{
    using System;
    using System.Collections.Generic;

    public interface ISpeechRecognizer
    {
        event Action<IReadOnlyList<IAlternative>> RecognizeCompleted;

        IDisposable Recognize();
    }
}