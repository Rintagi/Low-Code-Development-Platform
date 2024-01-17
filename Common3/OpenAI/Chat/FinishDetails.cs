using Newtonsoft.Json;

namespace OpenAI.Chat
{
    public sealed class FinishDetails
    {
        
        [JsonProperty("type")]
        public string Type { get; private set; }

        public override string ToString() { return Type; }

        public static implicit operator string(FinishDetails details) { return details == null ? null : details.ToString(); }
    }
}