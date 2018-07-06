using System.Xml.Serialization;

namespace Disorder.Unity.Core
{
    [XmlRoot("response")]
    public class DialogueResponseData
    {
        [XmlAttribute("id")] public int Id { get; set; }
        [XmlAttribute("destination")] public int DesinationId { get; set; }

        [XmlElement("content")] public string Content { get; set; }
    }
}