using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.Chat
{
    public sealed class ChatResponse : BaseResponse
    {
        public ChatResponse() { }

        internal ChatResponse(ChatResponse other) { CopyFrom(other); }

        /// <summary>
        /// A unique identifier for the chat completion.
        /// </summary>
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("object")]
        public string Object { get; private set; }

        [Obsolete("Use CreatedAtUnixTimeSeconds")]
        public int Created { get { return CreatedAtUnixTimeSeconds; } }

        /// <summary>
        /// The Unix timestamp (in seconds) of when the chat completion was created.
        /// </summary>
        
        [JsonProperty("created")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime; } }

        
        [JsonProperty("model")]
        public string Model { get; private set; }

        /// <summary>
        /// This fingerprint represents the backend configuration that the model runs with.
        /// Can be used in conjunction with the seed request parameter to understand when
        /// backend changes have been made that might impact determinism.
        /// </summary>
        
        [JsonProperty("system_fingerprint")]
        public string SystemFingerprint { get; private set; }

        
        [JsonProperty("usage")]
        public Usage Usage { get; private set; }

        [JsonIgnore]
        private List<Choice> choices;

        /// <summary>
        /// A list of chat completion choices. Can be more than one if n is greater than 1.
        /// </summary>
        
        [JsonProperty("choices")]
        public IReadOnlyList<Choice> Choices
        {
            get { return choices; }
            private set { choices = value.ToList(); }
        }

        [JsonIgnore]
        public Choice FirstChoice { get {  return Choices == null ? default(Choice) : Choices.FirstOrDefault(choice => choice.Index == 0); } }

        public override string ToString() { return FirstChoice == null ? string.Empty : FirstChoice.ToString() ?? string.Empty; }

        public static implicit operator string(ChatResponse response) { return response == null ? null : response.ToString(); }

        internal void CopyFrom(ChatResponse other)
        {
            if (other != null && !string.IsNullOrWhiteSpace(other.Id))
            {
                Id = other.Id;
            }

            if (other != null && !string.IsNullOrWhiteSpace(other.Object))
            {
                Object = other.Object;
            }

            if (other != null && !string.IsNullOrWhiteSpace(other.Model))
            {
                Model = other.Model;
            }

            if (other != null && other.Usage != null)
            {
                if (Usage == null)
                {
                    Usage = other.Usage;
                }
                else
                {
                    Usage.CopyFrom(other.Usage);
                }
            }

            if (other != null && other.Choices != null && other.Choices.Count > 0)
            {
                choices = choices ?? new List<Choice>();

                foreach (var otherChoice in other.Choices)
                {
                    if (otherChoice.Index + 1 > choices.Count)
                    {
                        choices.Insert(otherChoice.Index, otherChoice);
                    }

                    choices[otherChoice.Index].CopyFrom(otherChoice);
                }
            }
        }

        public string GetUsage(bool log = true)
        {
            var message = string.Format("{0} | {1} | {2}", Id, Model, Usage);

            if (log)
            {
                Console.WriteLine(message);
            }

            return message;
        }
    }
}
