// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Recognition.Speech
{
    using System.ComponentModel;

    public interface IAlternative
    {
        double Confidence { get; }
        string Transcript { get; }

    }
}