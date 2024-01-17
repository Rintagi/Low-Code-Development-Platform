using Newtonsoft.Json;
using OpenAI.Extensions;

namespace OpenAI.Chat
{
    public sealed class ResponseFormat
    {
        public ResponseFormat() { Type = ChatResponseFormat.Text; }

        public ResponseFormat(ChatResponseFormat format) { Type = format; }

        
        [JsonProperty("type")]
        [JsonConverter(typeof(JsonStringEnumConverter<ChatResponseFormat>))]
        public ChatResponseFormat Type { get; private set; }

        public static implicit operator ChatResponseFormat(ResponseFormat format) { return format.Type;}

        public static implicit operator ResponseFormat(ChatResponseFormat format) { return new ResponseFormat(format); }
    }
}