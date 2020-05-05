// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Configuration
{
    using Microsoft.CodeAnalysis.CSharp;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.IO;
    using System.Reflection;

    public sealed class ConfigProxy : DynamicObject, INotifyPropertyChanged
    {
        private struct Accessor
        {
            private readonly Delegate _get;
            private readonly Delegate _set;

            public Type Type { get; }

            public Accessor(Type type, Delegate get, Delegate set)
            {
                this.Type = type;
                this._get = get;
                this._set = set;
            }

            public object Get() => this._get.DynamicInvoke();

            public void Set(object value) => this._set.DynamicInvoke(value);
        }

        private readonly object _graph;

        private readonly IDictionary<string, Accessor> _list;

        private readonly FileInfo _meta;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<string> Names => this._list.Keys;

        public ConfigProxy(object graph)
        {
            var type = graph?.GetType() ?? throw new ArgumentNullException(nameof(graph));
            this._list = new Dictionary<string, Accessor>();
            this._meta = new FileInfo(GetName(type));

            foreach (var pinfo in type.GetProperties((BindingFlags)52))
                if (pinfo.GetCustomAttribute<FieldAttribute>() is FieldAttribute f &&
                    pinfo.GetGetMethod(true) is MethodInfo getter &&
                    pinfo.GetSetMethod(true) is MethodInfo setter)
                {
                    var getDele = Delegate.CreateDelegate(typeof(Func<>).MakeGenericType(getter.ReturnType), graph, getter);
                    var setDele = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(setter.GetParameters()[0].ParameterType), graph, setter);

                    var identifier =
                        f.Alias is string alias
                            ? SyntaxFacts.IsValidIdentifier(alias)
                                ? alias
                                : throw new ArgumentException("Invalid member name", alias)
                            : pinfo.Name;
                    this._list.Add(identifier, new Accessor(pinfo.PropertyType, getDele, setDele));
                }

            // metadata not found, create and serialize object to file.
            if (!this._meta.Exists)
            {
                this.Save();
            }

            this.Load();
            this._graph = graph;
        }

        public override bool Equals(object obj) => this._graph.Equals(obj);

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return this._list.Keys;
        }

        public override int GetHashCode() => this._graph.GetHashCode();

        public new Type GetType() => this._graph.GetType();

        public void Load()
        {
            using (var reader = new StreamReader(this._meta.OpenRead()))
            {
                var jObj = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());
                foreach (var jProp in jObj)
                {
                    if (this._list.TryGetValue(jProp.Key, out var a))
                    {
                        a.Set(jProp.Value.ToObject(a.Type));
                    }
                }
            }
        }

        public void Save()
        {
            using (var writer = new StreamWriter(this._meta.Open(FileMode.Create, FileAccess.ReadWrite)))
            {
                var j = new JObject();

                foreach (var accessor in this._list)
                {
                    j.Add(new JProperty(accessor.Key, accessor.Value.Get()));
                }

                var json = JsonConvert.SerializeObject(j, Formatting.Indented);
                writer.Write(json);
            }
        }

        public override string ToString() => this._graph.ToString();

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (indexes.Length.Equals(1) &&
                indexes[0] is string name)
            {
                if (this._list.TryGetValue(name, out var o))
                {
                    result = o.Get();
                    return true;
                }
                throw new KeyNotFoundException(name);
            }
            throw new ArgumentOutOfRangeException();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this._list.TryGetValue(binder.Name, out var accessor))
            {
                result = accessor.Get();
                return true;
            }
            throw new KeyNotFoundException(binder.Name);
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            if (indexes.Length.Equals(1) &&
                indexes[0] is string name)
            {
                if (this._list.TryGetValue(name, out var o))
                {
                    var re = o.Get();
                    if (re is null && value is null)
                        return true;

                    if (!re?.Equals(value) ?? !value?.Equals(re) ?? !re.Equals(value))
                    {
                        this._list[name].Set(value);
                        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
                    }

                    return true;
                }
                throw new KeyNotFoundException(name);
            }
            throw new ArgumentOutOfRangeException();
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (this._list.TryGetValue(binder.Name, out var o))
            {
                var re = o.Get();
                if (re is null && value is null)
                    return true;

                if (!re?.Equals(value) ?? !value?.Equals(re) ?? !re.Equals(value))
                {
                    this._list[binder.Name].Set(value);
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(binder.Name));
                }
                return true;
            }
            throw new KeyNotFoundException(binder.Name);
        }

        private static string GetName(Type type)
        {
            return type.GUID + ".meta";
        }
    }
}