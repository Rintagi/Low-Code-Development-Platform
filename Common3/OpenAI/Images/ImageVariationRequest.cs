using System;
using System.IO;

namespace OpenAI.Images
{
    public sealed class ImageVariationRequest : AbstractBaseImageRequest, IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="imagePath">
        /// The image to edit. Must be a valid PNG file, less than 4MB, and square.
        /// </param>
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
        public ImageVariationRequest(string imagePath, int numberOfResults = 1, ImageSize size = ImageSize.Large, string user = null, ResponseFormat responseFormat = ResponseFormat.Url)
            : this(File.OpenRead(imagePath), Path.GetFileName(imagePath), numberOfResults, size, user, responseFormat)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="image">
        /// The image to edit. Must be a valid PNG file, less than 4MB, and square.
        /// </param>
        /// <param name="imageName">
        /// The name of the image.
        /// </param>
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
        public ImageVariationRequest(Stream image, string imageName, int numberOfResults = 1, ImageSize size = ImageSize.Large, string user = null, ResponseFormat responseFormat = ResponseFormat.Url)
            : base(numberOfResults, size, responseFormat, user)
        {
            Image = image;

            if (string.IsNullOrWhiteSpace(imageName))
            {
                const string defaultImageName = "image.png";
                imageName = defaultImageName;
            }

            ImageName = imageName;

            if (numberOfResults  > 10 ||  numberOfResults < 1)
            {
                throw new ArgumentOutOfRangeException("numberOfResults", "The number of results must be between 1 and 10");
            }
        }

        ~ImageVariationRequest() { Dispose(false); }

        /// <summary>
        /// The image to use as the basis for the variation(s). Must be a valid PNG file, less than 4MB, and square.
        /// </summary>
        public Stream Image { get; private set; }

        public string ImageName { get; private set; }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Image != null) Image.Close();
                if (Image != null) Image.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
