using OpenAI.Extensions;
using System;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class Content
    {
        
        [JsonProperty("type")]
        [JsonConverter(typeof(JsonStringEnumConverter<ContentType>))]
        public ContentType Type { get; private set; }

        
        [JsonProperty("text")]
        public TextContent Text { get; private set; }

        
        [JsonProperty("image_url")]
        public ImageUrl ImageUrl { get; private set; }

        public override string ToString()
        {
            if (Type == ContentType.Text) return Text.Value;
            else if (Type == ContentType.ImageUrl) return ImageUrl.Url;
            else throw new ArgumentOutOfRangeException();
        }
    }
}