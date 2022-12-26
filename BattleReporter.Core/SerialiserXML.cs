using System.IO;
using System.Xml.Serialization;
namespace BattleReporter.Core
{
    public class SerialiserXML : ISerialiser
    {
        public T DeserialiseFromFile<T>(string path)
        {
            Stream fileStream = File.OpenRead(path);
            XmlSerializer reader = new(typeof(T));
            T? deserialisedObject = (T)reader.Deserialize(fileStream);
            fileStream.Close();
            return deserialisedObject;
        }

        public void SerialiseToFile<T>(string filePath, T item)
        {
            var fileStream = File.Create(filePath);
            XmlSerializer writer = new(typeof(T));
            writer.Serialize(fileStream, item);
            fileStream.Close();
        }

        public string SerialiseToString<T>(T item)
        {
            XmlSerializer writer = new(typeof(T));
            var output = new StringWriter();
            writer.Serialize(output, item);
            return output.ToString();
        }
    }
}