using System.Xml.Serialization;

namespace UnityCore
{
    [XmlInclude(typeof(DialogueTreeData))]
    [XmlRoot("trees")]
    public class DialoguesTreesListData
    {
        [XmlElement("tree")] public DialogueTreeData[] DialogueTrees;
    }
}