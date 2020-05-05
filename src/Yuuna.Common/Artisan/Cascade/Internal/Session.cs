// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Artisan.Cascade
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    internal sealed class Session : IInternalSession
    {
        private readonly object _lockObject;
        private readonly Queue<object> _q;

        event Action<Queue<object>> IInternalEventDataBus.Completed
        {
            add => this._completed += value;
            remove => this._completed -= value;
        }

        private event Action<Queue<object>> _completed;

        public int Count
        {
            get
            {
                lock (this._lockObject)
                    return this._q.Count;
            }
        }

        internal Session()
        {
            this._lockObject = new object();
            this._q = new Queue<object>();
        }

        public IEnumerator<object> GetEnumerator() => this._q.ToImmutableList().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        void IInternalEventDataBus.OnComplete() => this._completed?.Invoke(this._q);

        void IInternalStorage.Store<T>(T data)
        {
            lock (this._lockObject)
                this._q.Enqueue(data);
        }
    }
}