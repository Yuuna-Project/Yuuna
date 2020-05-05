// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com
namespace Yuuna.Common.Serialization
{
    using System.IO;

    public interface ISerializer
    {
        bool TryDeserialize<T>(TextReader reader, out T result);

        bool TrySerialize<T>(TextWriter writer, T graph);
    }
}