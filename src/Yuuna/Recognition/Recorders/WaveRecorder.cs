// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Recognition.Speech
{
    using NAudio.Wave;

    using System;
    using System.IO;

    using Yuuna.Contracts.Recorders;

    public class WaveRecorder : IRecorder
    {
        private readonly object _lock = new object();
        private readonly MemoryStream _rawStream;
        private readonly WaveInEvent _wave;
        private volatile bool _started = false;

        public event Action<Stream> Completed;

        public WaveRecorder()
        {
            this._wave = new WaveInEvent { WaveFormat = new WaveFormat(16000, 16, 1) };
            this._rawStream = new MemoryStream();

            this._wave.DataAvailable += this.OnDataAvailable;
        }

        public void Start()
        {
            if (this._started)
                return;

            lock (this._lock)
            {
                if (this._started)
                    return;

                this._wave.StartRecording();
                this._started = true;
            }
        }

        public void Stop()
        {
            if (!this._started)
                return;

            lock (this._lock)
            {
                if (!this._started)
                    return;

                this._wave.StopRecording();
                var wavStream = new RawSourceWaveStream(this._rawStream, this._wave.WaveFormat);
                wavStream.Seek(0, SeekOrigin.Begin);
                this.Completed?.Invoke(wavStream);

                this._started = false;
            }
        }

        protected virtual void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i++)
            {
                this._rawStream.WriteByte(e.Buffer[i]);
            }
        }
    }
}