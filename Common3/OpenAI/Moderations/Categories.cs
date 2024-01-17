using Newtonsoft.Json;

namespace OpenAI.Moderations
{
    public sealed class Categories
    {
        
        [JsonProperty("hate")]
        public bool Hate { get; private set; }

        
        [JsonProperty("hate/threatening")]
        public bool HateThreatening { get; private set; }

        
        [JsonProperty("harassment")]
        public bool Harassment { get; private set; }

        
        [JsonProperty("harassment/threatening")]
        public bool HarassmentThreatening { get; private set; }

        
        [JsonProperty("self-harm")]
        public bool SelfHarm { get; private set; }

        
        [JsonProperty("self-harm/intent")]
        public bool SelfHarmIntent { get; private set; }

        
        [JsonProperty("self-harm/instructions")]
        public bool SelfHarmInstructions { get; private set; }

        
        [JsonProperty("sexual")]
        public bool Sexual { get; private set; }

        
        [JsonProperty("sexual/minors")]
        public bool SexualMinors { get; private set; }

        
        [JsonProperty("violence")]
        public bool Violence { get; private set; }

        
        [JsonProperty("violence/graphic")]
        public bool ViolenceGraphic { get; private set; }
    }
}
