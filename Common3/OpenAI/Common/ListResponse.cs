using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI
{
    public sealed class ListResponse<TObject> : BaseResponse, IListResponse<TObject>
        where TObject : BaseResponse
    {
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("data")]
        public IReadOnlyList<TObject> Items { get; private set; }

        
        [JsonProperty("has_more")]
        public bool HasMore { get; private set; }

        
        [JsonProperty("first_id")]
        public string FirstId { get; private set; }

        
        [JsonProperty("last_id")]
        public string LastId { get; private set; }
    }
}
