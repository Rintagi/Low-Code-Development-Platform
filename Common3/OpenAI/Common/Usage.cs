using Newtonsoft.Json;

namespace OpenAI
{
    public sealed class Usage
    {
        public Usage() { }

        public Usage(int promptTokens, int completionTokens, int totalTokens)
        {
            PromptTokens = promptTokens;
            CompletionTokens = completionTokens;
            TotalTokens = totalTokens;
        }

        
        [JsonProperty("prompt_tokens")]
        public int? PromptTokens { get; private set; }

        
        [JsonProperty("completion_tokens")]
        public int? CompletionTokens { get; private set; }

        
        [JsonProperty("total_tokens")]
        public int? TotalTokens { get; private set; }

        internal void CopyFrom(Usage other)
        {
            if (other != null && other.PromptTokens != null)
            {
                PromptTokens = other.PromptTokens.Value;
            }

            if (other != null && other.CompletionTokens != null)
            {
                CompletionTokens = other.CompletionTokens.Value;
            }

            if (other != null && other.TotalTokens != null)
            {
                TotalTokens = other.TotalTokens.Value;
            }
        }

        public override string ToString() { return Newtonsoft.Json.JsonConvert.SerializeObject(this, OpenAIClient.jsonSerializationOptions); }

        public static Usage operator +(Usage a, Usage b)
        {
            return new Usage(
                  (a.PromptTokens ?? 0) + (b.PromptTokens ?? 0),
                  (a.CompletionTokens ?? 0) + (b.CompletionTokens ?? 0),
                  (a.TotalTokens ?? 0) + (b.TotalTokens ?? 0));
        }
    }
}
