// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Serialization
{
    using Nett;

    using System.IO;

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