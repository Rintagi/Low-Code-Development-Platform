using Newtonsoft.Json;

namespace OpenAI.Moderations
{
    public sealed class ModerationResult
    {
        
        [JsonProperty("categories")]
        public Categories Categories { get; private set; }

        
        [JsonProperty("category_scores")]
        public Scores Scores { get; private set; }

        
        [JsonProperty("flagged")]
        public bool Flagged { get; private set; }
    }
}
