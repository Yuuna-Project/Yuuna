
namespace Yuuna.Common.Serialization
{
    using System.IO;
    using System.Xml;

    internal sealed class XmlSerializer : Serializer
    {
        protected override T OnDeserialize<T>(TextReader reader)
        {
            var xdoc = new XmlDocument(); 
            xdoc.LoadXml(reader.ReadToEnd());
            var x = new XmlNodeReader(xdoc.DocumentElement);
            var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var obj = ser.Deserialize(x);
            return (T)obj;
        }
        protected override void OnSerialize<T>(TextWriter writer, T graph)
        {
            var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            ser.Serialize(writer, graph);
        }
    }
}
