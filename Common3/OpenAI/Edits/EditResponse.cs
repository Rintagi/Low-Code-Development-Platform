using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Edits
{
    [Obsolete("deprecated")]
    public sealed class EditResponse : BaseResponse
    {
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("created")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [Obsolete("use CreatedAtUnixTimeSeconds")]
        public int CreatedUnixTime { get { return CreatedAtUnixTimeSeconds;} }

        [JsonIgnore]
        [Obsolete("use CreatedAt")]
        public DateTime Created { get { return CreatedAt;} }

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime; } }

        
        [JsonProperty("choices")]
        public IReadOnlyList<Choice> Choices { get; private set; }

        
        [JsonProperty("usage")]
        public Usage Usage { get; private set; }

        /// <summary>
        /// Gets the text of the first edit, representing the main result
        /// </summary>
        public override string ToString()
        { return Choices.Count > 0
                ? Choices[0]
                : "Edit result has no valid output";
        }
    }
}
