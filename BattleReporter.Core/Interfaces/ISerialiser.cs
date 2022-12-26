namespace BattleReporter.Core
{
    public interface ISerialiser
    {
        T DeserialiseFromFile<T>(string path);
        void SerialiseToFile<T>(string filePath, T item);
        public string SerialiseToString<T>(T item);

    }
}