using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Moderations
{
    public sealed class ModerationsResponse : BaseResponse
    {
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("model")]
        public string Model { get; private set; }

        
        [JsonProperty("results")]
        public IReadOnlyList<ModerationResult> Results { get; private set; }
    }
}
