using Newtonsoft.Json;
using OpenAI.Extensions;

namespace OpenAI.Chat
{
    public sealed class Content
    {
        public Content() { }

        public Content(ImageUrl imageUrl)
        {
            Type = ContentType.ImageUrl;
            ImageUrl = imageUrl;
        }

        public Content(string input)
        {
            Type = ContentType.Text;
            Text = input;
        }

        public Content(ContentType type, string input)
        {
            Type = type;

            switch (Type)
            {
                case ContentType.Text:
                    Text = input;
                    break;
                case ContentType.ImageUrl:
                    ImageUrl = new ImageUrl(input);
                    break;
            }
        }

        
        [JsonProperty("type")]
        [JsonConverter(typeof(JsonStringEnumConverter<ContentType>))]
        public ContentType Type { get; private set; }

        
        [JsonProperty("text")]
        public string Text { get; private set; }

        
        [JsonProperty("image_url")]
        public ImageUrl ImageUrl { get; private set; }

        public bool ShouldSerializeText() { return Text != null; }
        public bool ShouldSerializeImageUrl() { return ImageUrl != null; }

        public static implicit operator Content(string input) { return new Content(ContentType.Text, input); }

        public static implicit operator Content(ImageUrl imageUrl) { return new Content(imageUrl); }
    }
}