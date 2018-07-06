using System.Text;

namespace Disorder.Unity.Core
{
    public static class XmlUtils
    {     
        public static string ByteArrToString(byte[] bytes) 
        {      
            UTF8Encoding encoding = new UTF8Encoding(); 
            return encoding.GetString(bytes); 
        } 
 
        public static byte[] StringToByteArr(string xmlString) 
        { 
            UTF8Encoding encoding = new UTF8Encoding(); 
            return encoding.GetBytes(xmlString); 
        } 
    }
}