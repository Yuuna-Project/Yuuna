
namespace Yuuna.Common.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public abstract class Serializer : ISerializer
    {
        public bool TryDeserialize<T>(TextReader reader, out T result)
        {
            try
            {
                result = this.OnDeserialize<T>(reader);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        protected abstract T OnDeserialize<T>(TextReader reader);

        public bool TrySerialize<T>(TextWriter writer, T graph)
        {
            try
            {
                this.OnSerialize<T>(writer, graph);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected abstract void OnSerialize<T>(TextWriter writer, T graph);


    }
}