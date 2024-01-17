using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Embeddings
{
    public sealed class Datum
    {
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("embedding")]
        public IReadOnlyList<double> Embedding { get; private set; }

        
        [JsonProperty("index")]
        public int Index { get; private set; }
    }
}
