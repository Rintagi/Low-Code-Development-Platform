using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Completions
{
    /// <summary>
    /// Object belonging to a <see cref="Choice"/>
    /// </summary>
    public sealed class LogProbabilities
    {
        
        [JsonProperty("tokens")]
        public IReadOnlyList<string> Tokens { get; private set; }

        
        [JsonProperty("token_logprobs")]
        public IReadOnlyList<double> TokenLogProbabilities { get; private set; }

        
        [JsonProperty("top_logprobs")]
        public IReadOnlyList<IReadOnlyDictionary<string, double>> TopLogProbabilities { get; private set; }

        
        [JsonProperty("text_offset")]
        public IReadOnlyList<int> TextOffsets { get; private set; }
    }
}
