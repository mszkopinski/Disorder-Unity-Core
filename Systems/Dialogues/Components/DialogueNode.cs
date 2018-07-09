using System.Collections.Generic;
using System.Linq;

namespace UnityCore
{
    public class DialogueNode
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public List<DialogueResponse> Options { get; private set; }

        public DialogueNode()
        {
        }

        public DialogueNode(int id, string title, string content, List<DialogueResponse> options)
        {
            Id = id;
            Title = title;
            Content = content;
            Options = options;
        }

        #region Override

        public DialogueNode(DialogueNodeData data)
            : this(data.Id, data.Title, data.Content, data.DialogueResponses.Select(o => new DialogueResponse(o)).ToList())
        {
        }

        public override bool Equals(object obj)
        {
            var node = obj as DialogueNode;
            return node != null
                   && Id == node.Id;
        }

        public override int GetHashCode()
        {
            var hashCode = -1492648413;
            hashCode = hashCode * -1541538291 + EqualityComparer<int>.Default.GetHashCode(Id);
            return hashCode;
        }

        #endregion
    }
}