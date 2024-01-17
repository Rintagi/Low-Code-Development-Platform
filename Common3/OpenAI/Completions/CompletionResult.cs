using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.Completions
{
    /// <summary>
    /// Represents a result from calling the <see cref="CompletionsEndpoint"/>.
    /// </summary>
    [Obsolete("use CompletionResponse")]
    public sealed class CompletionResult : BaseResponse
    {
        /// <summary>
        /// The identifier of the result, which may be used during troubleshooting
        /// </summary>
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("object")]
        public string Object { get; private set; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        
        [JsonProperty("created")]
        public int CreatedUnixTime { get; private set; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime Created { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTime).DateTime; } }

        
        [JsonProperty("model")]
        public string Model { get; private set; }

        /// <summary>
        /// The completions returned by the API.  Depending on your request, there may be 1 or many choices.
        /// </summary>
        
        [JsonProperty("choices")]
        public IReadOnlyList<Choice> Completions { get; private set; }

        [JsonIgnore]
        public Choice FirstChoice { get { return Completions == null ? null : Completions.FirstOrDefault(choice => choice.Index == 0); }}

        public override string ToString() { return FirstChoice == null ? string.Empty : FirstChoice.ToString() ?? string.Empty; }

        public static implicit operator string(CompletionResult result) { return result == null ? null : result.ToString(); }

        public static implicit operator CompletionResponse(CompletionResult result) { return new CompletionResponse(result); }
    }
}
