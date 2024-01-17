using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Embeddings
{
    public sealed class EmbeddingsResponse : BaseResponse
    {
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("data")]
        public IReadOnlyList<Datum> Data { get; private set; }

        
        [JsonProperty("model")]
        public string Model { get; private set; }

        
        [JsonProperty("usage")]
        public Usage Usage { get; private set; }
    }
}
