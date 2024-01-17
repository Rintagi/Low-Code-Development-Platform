using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.FineTuning
{
    [Obsolete("Use ListResponse<EventResponse>")]
    public sealed class EventList
    {
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("data")]
        public IReadOnlyList<Event> Events { get; private set; }

        
        [JsonProperty("has_more")]
        public bool HasMore { get; private set; }
    }
}