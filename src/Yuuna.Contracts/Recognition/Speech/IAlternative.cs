// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Recognition.Speech
{
    public interface IAlternative
    {
        double Confidence { get; }
        string Transcript { get; }
    }
}