using Newtonsoft.Json;

namespace OpenAI
{
    public sealed class ImageUrl
    {
        [JsonConstructor]
        public ImageUrl(string url, ImageDetail detail = ImageDetail.Auto)
        {
            Url = url;
            Detail = detail;
        }

        
        [JsonProperty("url")]
        public string Url { get; private set; }

        
        [JsonProperty("detail")]
        public ImageDetail Detail { get; private set; }

        public bool ShouldSerializeUrl()
        {
            return !string.IsNullOrEmpty(Url);
        }
        public bool ShouldSerializeDetail()
        {
            return Detail != default(ImageDetail);
        }
    }
}