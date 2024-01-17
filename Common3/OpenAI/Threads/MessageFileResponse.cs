using System;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class MessageFileResponse : BaseResponse
    {
        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// The object type, which is always thread.message.file.
        /// </summary>
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the message file was created.
        /// </summary>
        
        [JsonProperty("created_at")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime; } }

        /// <summary>
        /// The ID of the message that the File is attached to.
        /// </summary>
        
        [JsonProperty("message_id")]
        public string MessageId { get; private set; }

        public static implicit operator string(MessageFileResponse response) { return response == null ? null : response.ToString(); }

        public override string ToString() { return Id; }
    }
}