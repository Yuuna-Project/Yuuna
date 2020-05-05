// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Serialization
{
    using System.IO;

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

        protected abstract T OnDeserialize<T>(TextReader reader);

        protected abstract void OnSerialize<T>(TextWriter writer, T graph);
    }
}