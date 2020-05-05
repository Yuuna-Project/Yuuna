// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Recognition.Speech
{
    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Speech.V1;

    using Grpc.Auth;
    using Grpc.Core;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Yuuna.Contracts.Recognition.Speech;
    using Yuuna.Contracts.Recorders;

    public sealed class SpeechRecognizer : ISpeechRecognizer
    {
        private readonly RecognitionConfig _config;
        private readonly object _lock = new object();
        private readonly IRecorder _recorder;
        private readonly SpeechClient _speech;
        private volatile bool _started;

        public event Action<IReadOnlyList<IAlternative>> RecognizeCompleted;

        private SpeechRecognizer(FileInfo secret)
        {
            if (secret is null)
                throw new ArgumentNullException(nameof(secret));

            if (!secret.Exists)
                throw new FileNotFoundException("secret file not found.", secret.FullName);

            var credential = GoogleCredential.FromFile(secret.FullName);
            var channel = new Channel(SpeechClient.DefaultEndpoint.Host, credential.ToChannelCredentials());
            this._speech = SpeechClient.Create(channel);
            this._config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16_000,
                LanguageCode = Thread.CurrentThread.CurrentCulture.Name,
            };
            this._recorder = new WaveRecorder();
            this._recorder.Completed += this.OnComplete;
        }

        /// <summary>
        /// 建立新 <see cref="ISpeechRecognizer"/> 實體。
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static ISpeechRecognizer Create(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("secret is null or empty.", nameof(secret));
            return new SpeechRecognizer(new FileInfo(secret));
        }

        public IDisposable Recognize()
        {
            lock (this._lock)
            {
                if (this._started)
                    return null;
                this._started = true;
                return new Duration(this._recorder, () =>
                {
                    this._started = false;
                });
            }
        }

        private void OnComplete(Stream stream)
        {
            lock (this._lock)
            {
                var wavStream = RecognitionAudio.FromStream(stream);
                var response = this._speech.Recognize(this._config, wavStream);

                var alternatives =
                    from r in response.Results
                    from a in r.Alternatives
                    select new AlternativeBridge(a) as IAlternative;

                this.RecognizeCompleted?.Invoke(alternatives.ToList());
            }
        }
    }
}