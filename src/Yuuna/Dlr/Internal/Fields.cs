namespace Yuuna.Dlr.Internal
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class Fields : DynamicObject, INotifyPropertyChanged, IDictionary<string, object>, ISerializable
    {
        private readonly Dictionary<string, object> _fields;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count => this._fields.Count;

        public bool IsReadOnly => ((IDictionary<string, object>)this._fields).IsReadOnly;

        public ICollection<string> Keys => this._fields.Keys;

        public ICollection<object> Values => this._fields.Values;

        public object this[string key] { get => this._fields[key]; set => this._fields[key] = value; }

        internal Fields(Dictionary<string, object> fields)
        {
            this._fields = fields;
        }

        public void Add(string key, object value) => this._fields.Add(key, value);

        public void Add(KeyValuePair<string, object> item) => ((IDictionary<string, object>)this._fields).Add(item);

        public void Clear() => this._fields.Clear();

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this._fields.Contains(item);
        }

        public bool ContainsKey(string key) => this._fields.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => ((IDictionary<string, object>)this._fields).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => this._fields.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this._fields.GetEnumerator();

        public void GetObjectData(SerializationInfo info, StreamingContext context) => this._fields.GetObjectData(info, context);

        public bool Remove(string key) => this._fields.Remove(key);

        public bool Remove(KeyValuePair<string, object> item) => ((IDictionary<string, object>)this._fields).Remove(item);

        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            if (this._fields.ContainsKey(binder.Name))
                this._fields.Remove(binder.Name);

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            this._fields.TryGetValue(binder.Name, out result);
            return true;
        }

        public bool TryGetValue(string key, out object value) => this._fields.TryGetValue(key, out value);

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this._fields[binder.Name] = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(binder.Name));

            return true;
        }
    }
}
