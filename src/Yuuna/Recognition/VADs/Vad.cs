
namespace Yuuna.Recognition.VADs
{
    using NAudio.Wave;

    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public class Vad
    {
        private readonly object _lock = new object();
        private readonly WaveInEvent _wave;
        private volatile bool _started = false;


        public Vad()
        {
            this._wave = new WaveInEvent
            {
                WaveFormat = new WaveFormat(16000, 16, 1),
                BufferMilliseconds = 50
            };

            this._wave.DataAvailable += this.OnDataAvailable;
            this.Threshold = null;
        }
         

        [DefaultValue(-55.0)]
        public double? Threshold
        {
            get => this._threshold;
            set => this._threshold = value.HasValue ? value : (double)typeof(Vad).GetProperty(nameof(this.Threshold)).GetCustomAttribute<DefaultValueAttribute>().Value;
        }

        private double? _threshold;


        public event EventHandler<VadStateEventArgs> AudioStateChanged;

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

        private volatile bool _state;

        protected virtual void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            const int _shortSize = sizeof(short);
            var sum = default(double);

            var count = e.BytesRecorded / _shortSize;

            for (var index = default(int); index < count; index += _shortSize)
            {
                double sample = BitConverter.ToInt16(e.Buffer, index) / 32768.0;
                sum += sample * sample;
            }

            var rms = Math.Sqrt(sum / (count / 2.0));
            var decibel = Math.Round(20.0 * Math.Log10(rms), 2);

            var state = decibel > this.Threshold;

            if (this._state != state)
            {
                if (state)
                    AudioStateChanged?.Invoke(this, new VadStateEventArgs(VadState.Detect, decibel));
                else
                    AudioStateChanged?.Invoke(this, new VadStateEventArgs(VadState.Idle, this.Threshold.Value));

                this._state = state;
            }
            else if (state)
            {
                AudioStateChanged?.Invoke(this, new VadStateEventArgs(VadState.Detect, decibel));
            }

        }

    }

}
