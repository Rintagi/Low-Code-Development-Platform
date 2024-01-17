using OpenAI.Extensions;
using System;
using Newtonsoft.Json;

namespace OpenAI.Images
{
    /// <summary>
    /// Abstract base image class for making requests.
    /// </summary>
    public abstract class AbstractBaseImageRequest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="numberOfResults">
        /// The number of images to generate. Must be between 1 and 10.
        /// </param>
        /// <param name="size">
        /// The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024.
        /// </param>
        /// <param name="user">
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </param>
        /// <param name="responseFormat">
        /// The format in which the generated images are returned.
        /// Must be one of url or b64_json.
        /// <para/> Defaults to <see cref="ResponseFormat.Url"/>
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected AbstractBaseImageRequest(int numberOfResults = 1, ImageSize size = ImageSize.Large, ResponseFormat responseFormat = ResponseFormat.Url, string user = null)
        {
            Number = numberOfResults;

            string sizeText;
            if (size == ImageSize.Small)
            {
                sizeText = "256x256";
            }
            else if (size == ImageSize.Medium)
            {
                sizeText = "512x512";
            }
            else if (size == ImageSize.Large)
            {
                sizeText = "1024x1024";
            }
            else
            {
                throw new ArgumentOutOfRangeException("size", size, null);
            }

            Size = sizeText;
            User = user;
            ResponseFormat = responseFormat;
        }

        /// <summary>
        /// The number of images to generate. Must be between 1 and 10.
        /// </summary>
        [JsonProperty("n")]
        public int Number { get; private set; }

        /// <summary>
        /// The format in which the generated images are returned.
        /// Must be one of url or b64_json.
        /// <para/> Defaults to <see cref="ResponseFormat.Url"/>
        /// </summary>
        [JsonProperty("response_format")]
        [JsonConverter(typeof(JsonStringEnumConverter<ResponseFormat>))]
        public ResponseFormat ResponseFormat { get; private set; }

        /// <summary>
        /// The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; private set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </summary>
        [JsonProperty("user")]
        public string User { get; private set; }
    }
}