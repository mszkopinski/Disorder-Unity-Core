namespace Disorder.Unity.Core
{
    public class DialogueResponse
    {
        public string Content { get; private set; }
        public int Id { get; private set; }
        public int DestinationNodeId { get; private set; }

        public DialogueResponse()
        {
        }

        public DialogueResponse(int id, int destinationNodeId, string content)
        {
            Id = id;
            DestinationNodeId = destinationNodeId;
            Content = content;
        }

        public DialogueResponse(DialogueResponseData data)
            : this(data.Id, data.DesinationId, data.Content)
        {
        }

        #region Override

        public override bool Equals(object obj)
        {
            var response = obj as DialogueResponse;
            return response != null &&
                   Id == response.Id &&
                   DestinationNodeId == response.DestinationNodeId;
        }

        public override int GetHashCode()
        {
            var hashCode = -666034766;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + DestinationNodeId.GetHashCode();
            return hashCode;
        }

        #endregion
    }
}