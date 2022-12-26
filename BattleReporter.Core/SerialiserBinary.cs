using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BattleReporter.Core
{
    public class SerialiserBinary : ISerialiser
    {
        //Writer
        public void SerialiseToFile<T>(string filePath, T item)
        {
            var fileStream = File.Create(filePath);
            BinaryFormatter writer = new();
            writer.Serialize(fileStream, item);
            fileStream.Close();
        }
        //Reader
        public T DeserialiseFromFile<T>(string path)
        {
            Stream fileStream = File.OpenRead(path);
            BinaryFormatter reader = new();
            T? deserialisedObject = (T)reader.Deserialize(fileStream);
            fileStream.Close();
            return deserialisedObject;
        }

        public string SerialiseToString<T>(T item)
        {
            throw new NotImplementedException();
        }
    }
}
