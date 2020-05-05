// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Recognition.Speech
{
    using Google.Cloud.Speech.V1;

    using Yuuna.Contracts.Recognition.Speech;

    internal struct AlternativeBridge : IAlternative
    {
        public double Confidence { get; }

        public string Transcript { get; }

        internal AlternativeBridge(SpeechRecognitionAlternative s)
        {
            this.Confidence = s.Confidence;
            this.Transcript = s.Transcript;
        }
    }
}