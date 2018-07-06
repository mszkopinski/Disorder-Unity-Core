namespace Disorder.Unity.Core
{
    public interface IEncryptedXmlSerializer
    {
        bool UseEncryption { get; set; }

        T DecryptAndDeserialize<T>(string path) where T : ISerializable;
        void EncryptAndSerialize<T>(string path, object serializableObject) where T : ISerializable;
    }
}