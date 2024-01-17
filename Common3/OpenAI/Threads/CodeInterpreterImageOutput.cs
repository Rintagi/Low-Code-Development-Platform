using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class CodeInterpreterImageOutput
    {
        /// <summary>
        /// The file ID of the image.
        /// </summary>
        
        [JsonProperty("file_id")]
        public string FileId { get; private set; }
    }
}