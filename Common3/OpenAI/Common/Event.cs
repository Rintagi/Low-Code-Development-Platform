using System;
using Newtonsoft.Json;

namespace OpenAI
{
    [Obsolete("use EventResponse")]
    public sealed class Event : BaseResponse
    {
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("created_at")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [Obsolete("use CreatedAtUnixTimeSeconds")]
        public int CreatedAtUnixTime { get {return CreatedAtUnixTimeSeconds; }}

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime; } }

        
        [JsonProperty("level")]
        public string Level { get; private set; }

        
        [JsonProperty("message")]
        public string Message { get; private set; }

        public static implicit operator EventResponse(Event @event) { return new EventResponse(@event); }
    }
}
