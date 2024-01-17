using Newtonsoft.Json;

namespace OpenAI.Chat
{
    public sealed class Choice
    {
        
        [JsonProperty("message")]
        public Message Message { get; private set; }

        
        [JsonProperty("delta")]
        public Delta Delta { get; private set; }

        
        [JsonProperty("finish_reason")]
        public string FinishReason { get; private set; }

        
        [JsonProperty("finish_details")]
        public FinishDetails FinishDetails { get; private set; }

        
        [JsonProperty("index")]
        public int Index { get; private set; }

        public bool ShouldSerializeMessage() { return Message != null; }
        public bool ShouldSerializeDelta() { return Delta != null; }

        public override string ToString() {return Message != null && Message.Content != null ? Message.Content.ToString():
            Delta != null && Delta.Content!= null ? Delta.Content : string.Empty; }

        public static implicit operator string(Choice choice) { return choice == null ? null : choice.ToString(); }

        internal void CopyFrom(Choice other)
        {
            if (other != null && other.Message != null)
            {
                Message = other.Message;
            }

            if (other != null && other.Delta != null)
            {
                if (Message == null)
                {
                    Message = new Message(other.Delta);
                }
                else
                {
                    Message.CopyFrom(other.Delta);
                }
            }

            if (other != null && !string.IsNullOrWhiteSpace(other.FinishReason))
            {
                FinishReason = other.FinishReason;
            }

            if (other != null && other.FinishDetails != null)
            {
                FinishDetails = other.FinishDetails;
            }

            Index = other == null ? 0 : other.Index;
        }
    }
}
