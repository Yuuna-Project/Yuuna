// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Serialization
{
    using Newtonsoft.Json;

    using System.IO;

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