using System;
using Newtonsoft.Json;

namespace OpenAI.Models
{
    public sealed class Permission
    {
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("created")]
        public int CreatedAtUnitTimeSeconds { get; private set; }

        [Obsolete("use CreatedAtUnitTimeSeconds")]
        public int CreatedAtUnixTime { get { return CreatedAtUnitTimeSeconds; }}

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnitTimeSeconds).DateTime; } }

        
        [JsonProperty("allow_create_engine")]
        public bool AllowCreateEngine { get; private set; }

        
        [JsonProperty("allow_sampling")]
        public bool AllowSampling { get; private set; }

        
        [JsonProperty("allow_logprobs")]
        public bool AllowLogprobs { get; private set; }

        
        [JsonProperty("allow_search_indices")]
        public bool AllowSearchIndices { get; private set; }

        
        [JsonProperty("allow_view")]
        public bool AllowView { get; private set; }

        
        [JsonProperty("allow_fine_tuning")]
        public bool AllowFineTuning { get; private set; }

        
        [JsonProperty("organization")]
        public string Organization { get; private set; }

        
        [JsonProperty("group")]
        public object Group { get; private set; }

        
        [JsonProperty("is_blocking")]
        public bool IsBlocking { get; private set; }
    }
}
