



namespace Yuuna.Common.Serialization
{
    using System.IO;
    using Nett;

    internal sealed class TomlSerializer : Serializer
    {
        protected override T OnDeserialize<T>(TextReader reader)
        {
            return Toml.ReadString<T>(reader.ReadToEnd());
        }

        protected override void OnSerialize<T>(TextWriter writer, T graph)
        {
            writer.Write(Toml.WriteString(graph));
        }
    }
}
