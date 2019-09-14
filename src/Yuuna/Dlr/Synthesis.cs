namespace Yuuna.Dlr
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using Yuuna.Dlr.Internal;

    /// <summary>
    /// 提供一般物件至 DLR 物件的動態轉換
    /// </summary>
    [Serializable]
    public class Synthesis :
        IDynamicMetaObjectProvider,
        ISerializable,
        INotifyPropertyChanged,
        IEnumerable<KeyValuePair<string, object>>
    {

        private readonly IDynamicMetaObjectProvider _provider;

        /// <summary>
        /// 當物件成員的值變更時引發
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)this._provider).PropertyChanged += value;
            }
            remove
            {
                ((INotifyPropertyChanged)this._provider).PropertyChanged -= value;
            }
        }

        public dynamic this[string name]
        {
            get
            {
                return ((IDictionary<string, object>)this._provider).TryGetValue(name, out var v)
                    ? v
                    : throw new KeyNotFoundException("Key: " + name);
            }
        }

        /// <summary>
        /// 無建構參數的動態物件建構子
        /// </summary>
        public Synthesis() : this(null)
        {
        }

        /// <summary> 提供從 <see cref="IDictionary&lt;string, object"/> 轉換為動態物件的建構子 </summary> <param name="dictionary"></param>
        public Synthesis(IDictionary<string, object> dictionary)
        {
            var fields = default(Dictionary<string, object>);
            if (dictionary is null)
                fields = new Dictionary<string, object>();
            else if (dictionary is Dictionary<string, object> dict)
                fields = dict;
            else
                fields = new Dictionary<string, object>(dictionary);
            this._provider = new Fields(fields);
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return ((IDictionary<string, object>)this._provider).GetEnumerator();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, object>)this._provider).GetEnumerator();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
        {
            return new MetaObject(this._provider, parameter, BindingRestrictions.GetTypeRestriction(parameter, this.GetType()), this);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)this._provider).GetObjectData(info, context);
        }
    }
}