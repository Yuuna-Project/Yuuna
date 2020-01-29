


namespace Yuuna.Common.Serialization
{
    using System.IO;

    internal sealed class YamlSerializer : Serializer
    {
        
        protected override T OnDeserialize<T>(TextReader reader)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            return deserializer.Deserialize<T>(reader.ReadToEnd());
        }

        protected override void OnSerialize<T>(TextWriter writer, T graph)
        {
            var serializer = new YamlDotNet.Serialization.Serializer();
            writer.Write(serializer.Serialize(graph));
        }
    }
}
