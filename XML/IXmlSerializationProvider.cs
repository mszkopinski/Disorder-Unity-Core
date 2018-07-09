namespace UnityCore
{
    public interface IXmlSerializationProvider
    {
        string SerializeObject<T>(object obj);
        object DeserializeObject<T>(string xmlString);
        void CreateXmlFileOutput(string fileLocation, string fileName, string data);
        string LoadDataFromXml(string fileLocation, string fileName);
    }
}