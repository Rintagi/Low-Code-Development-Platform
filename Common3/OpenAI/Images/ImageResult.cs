using Newtonsoft.Json;

namespace OpenAI.Images
{
    public sealed class ImageResult
    {
        
        [JsonProperty("url")]
        public string Url { get; private set; }

        
        [JsonProperty("b64_json")]
        public string B64_Json { get; private set; }

        
        [JsonProperty("revised_prompt")]
        public string RevisedPrompt { get; private set; }

        public static implicit operator string(ImageResult result) { return result == null ? null : result.ToString(); }

        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(Url)
                  ? Url
                  : !string.IsNullOrWhiteSpace(B64_Json)
                      ? B64_Json : null;
        }
    }
}
