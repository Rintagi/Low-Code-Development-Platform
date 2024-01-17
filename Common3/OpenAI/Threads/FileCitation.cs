using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class FileCitation
    {
        /// <summary>
        /// The ID of the specific File the citation is from.
        /// </summary>
        
        [JsonProperty("file_id")]
        public string FileId { get; private set; }

        /// <summary>
        /// The specific quote in the file.
        /// </summary>
        
        [JsonProperty("quote")]
        public string Quote { get; private set; }
    }
}
