using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Disorder.Unity.Core
{
    public class XmlSerializationProvider : IXmlSerializationProvider
    {
        public string SerializeObject<T>(object obj)
        {
            try
            {
                string xmlString = null;

                var memoryStream = new MemoryStream();
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                serializer.Serialize(xmlTextWriter, obj);
                memoryStream = xmlTextWriter.BaseStream as MemoryStream;

                if (memoryStream != null)
                    xmlString = XmlUtils.ByteArrToString(memoryStream.ToArray());

                return xmlString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object DeserializeObject<T>(string xmlString)
        {
            try
            {
                byte[] bytes = XmlUtils.StringToByteArr(xmlString);

                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T), new XmlRootAttribute("trees"));
                var memoryStream = new MemoryStream(bytes);
                object deserializedObject = serializer.Deserialize(memoryStream);

                return deserializedObject;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateXmlFileOutput(string fileLocation, string fileName, string data)
        {
            StreamWriter writer;
            FileInfo fileInfo = new FileInfo(fileLocation + "\\" + fileName);

            if (fileInfo.Exists)
            {
                writer = fileInfo.CreateText();
            }
            else
            {
                fileInfo.Delete();
                writer = fileInfo.CreateText();
            }

            writer.Write(data);
            writer.Close();
        }

        public string LoadDataFromXml(string fileLocation, string fileName)
        {
            using (StreamReader r = File.OpenText(fileLocation + "\\" + fileName))
            {
                string data = r.ReadToEnd();
                return data;
            }
        }
    }
}