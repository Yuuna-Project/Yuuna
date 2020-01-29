

namespace Yuuna.Common.Serialization
{
    using System.IO;
    using Newtonsoft.Json;

    internal sealed class JsonSerializer : Serializer
    {
        protected override T OnDeserialize<T>(TextReader reader)
        {
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        protected override void OnSerialize<T>(TextWriter writer, T graph)
        {
            writer.Write(JsonConvert.SerializeObject(graph));
        }
    }
}
