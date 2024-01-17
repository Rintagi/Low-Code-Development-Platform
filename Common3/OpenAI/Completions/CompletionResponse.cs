using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.Completions
{
    /// <summary>
    /// Represents a result from calling the <see cref="CompletionsEndpoint"/>.
    /// </summary>
    public sealed class CompletionResponse : BaseResponse
    {
        public CompletionResponse() { }

#pragma warning disable 0618 // Type or member is obsolete
        internal CompletionResponse(CompletionResult result)
        {
            Id = result.Id;
            Object = result.Object;
            CreatedUnixTimeSeconds = result.CreatedUnixTime;
            Model = result.Model;
            Completions = result.Completions;
        }
#pragma warning restore 0618 // Type or member is obsolete

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
        public int CreatedUnixTimeSeconds { get; private set; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime Created { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTimeSeconds).DateTime; }}

        
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

        public static implicit operator string(CompletionResponse response) { return response == null ? null : response.ToString(); }
    }
}