using Newtonsoft.Json;

namespace OpenAI
{
    internal sealed class DeletedResponse
    {
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("deleted")]
        public bool Deleted { get; private set; }
    }
}