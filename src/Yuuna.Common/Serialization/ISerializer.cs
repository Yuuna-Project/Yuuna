namespace Yuuna.Common.Serialization
{
    using Newtonsoft.Json;
    using System.IO;

    public interface ISerializer
    {
        bool TryDeserialize<T>(TextReader reader, out T result);

        bool TrySerialize<T>(TextWriter writer, T graph);
    }
}
