
namespace Yuuna.Common.Serialization
{
    public static class Serializers
    {
        public readonly static ISerializer Json = new JsonSerializer();
        public readonly static ISerializer Toml = new TomlSerializer();
        public readonly static ISerializer Yaml = new YamlSerializer();
        public readonly static ISerializer Xml = new XmlSerializer();
    }
}