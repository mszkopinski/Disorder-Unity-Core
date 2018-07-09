using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace UnityCore
{
    public static class EncryptedXmlSerializer
    {
        public static bool UseEncryption { get; set; } = false;

        static readonly string PrivateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);

        public static T DecryptAndDeserialize<T>(string filePath) where T : ISerializable
        {
            T result;

            using (var reader = new StreamReader(filePath))
            {
                string decrypted = UseEncryption ? Decrypt(reader.ReadToEnd()) : reader.ReadToEnd();

                using (var memoryStream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(memoryStream) { AutoFlush = true })
                    {
                        writer.WriteLine(decrypted);
                        memoryStream.Position = 0;

                        var serializer = new XmlSerializer(typeof(T));
                        result = (T)serializer.Deserialize(memoryStream);
                    }
                }
            }

            return result;
        }

        public static void EncryptAndSerialize<T>(string filePath, object serializableObject) where T : ISerializable
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, serializableObject);

                memoryStream.Flush();
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream))
                {
                    string stringToEncrypt = UseEncryption ? Encrypt(reader.ReadToEnd()) : reader.ReadToEnd();

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        using (var writer = new StreamWriter(fileStream))
                        {
                            writer.WriteLine(stringToEncrypt);
                        }
                    }
                }

            }
        }

        static string Encrypt(string toEncrypt)
        {
#if UNITY_WP8
            return toEncrypt;
#else
            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rijndael = CreateRijndaelManaged();
            ICryptoTransform cryptoTransform = rijndael.CreateEncryptor();
            byte[] result = cryptoTransform.TransformFinalBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);

            return Convert.ToBase64String(result, 0, result.Length);
#endif
        }

        static string Decrypt(string toDecrypt)
        {
#if UNITY_WP8
            return toDecrypt;
#else
            byte[] bytesToEncrypt = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rijndael = CreateRijndaelManaged();
            ICryptoTransform cTransform = rijndael.CreateDecryptor();
            byte[] result = cTransform.TransformFinalBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);

            return Encoding.UTF8.GetString(result);
#endif
        }

        static RijndaelManaged CreateRijndaelManaged()
        {
#if UNITY_WP8
            return null;
#else
            byte[] keyArray = Encoding.UTF8.GetBytes(PrivateKey);
            var result = new RijndaelManaged();

            var newKeysArray = new byte[16];
            Array.Copy(keyArray, 0, newKeysArray, 0, 16);

            result.Key = newKeysArray;
            result.Mode = CipherMode.ECB;
            result.Padding = PaddingMode.PKCS7;

            return result;
#endif
        }
    }
}