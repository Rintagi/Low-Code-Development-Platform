using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Images
{
    internal sealed class ImagesResponse : BaseResponse
    {
        
        [JsonProperty("created")]
        public int Created { get; private set; }

        
        [JsonProperty("data")]
        public IReadOnlyList<ImageResult> Results { get; private set; }
    }
}
