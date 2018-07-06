using System.Xml.Serialization;

namespace Disorder.Unity.Core
{
    [XmlInclude(typeof(DialogueTreeData))]
    [XmlRoot("trees")]
    public class DialoguesTreesListData
    {
        [XmlElement("tree")] public DialogueTreeData[] DialogueTrees;
    }
}