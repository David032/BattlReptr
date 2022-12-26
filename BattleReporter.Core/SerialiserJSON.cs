using System.IO;
using Newtonsoft.Json;

namespace BattleReporter.Core
{
    public class SerialiserJSON : ISerialiser
    {
        public T DeserialiseFromFile<T>(string path)
        {
            string json = File.ReadAllText(path);
            var jsonObj = JsonConvert.DeserializeObject<T>(json);
            return jsonObj;
        }

        public void SerialiseToFile<T>(string filePath, T item)
        {
            string json = JsonConvert.SerializeObject(item);
            File.WriteAllText(filePath, json);
        }

        public string SerialiseToString<T>(T item)
        {
            string json = JsonConvert.SerializeObject(item);
            return json;
        }
    }
}