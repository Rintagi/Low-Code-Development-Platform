using System;
using Newtonsoft.Json;

namespace OpenAI.Files
{
    /// <summary>
    /// The File object represents a document that has been uploaded to OpenAI.
    /// </summary>
    [Obsolete("use FileResponse")]
    public sealed class FileData : BaseResponse
    {
        /// <summary>
        /// The file identifier, which can be referenced in the API endpoints.
        /// </summary>
        
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// The object type, which is always 'file'.
        /// </summary>
        
        [JsonProperty("object")]
        public string Object { get; private set; }

        /// <summary>
        /// The size of the file, in bytes.
        /// </summary>
        
        [JsonProperty("bytes")]
        public int Size { get; private set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the file was created.
        /// </summary>
        
        [JsonProperty("created_at")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        [Obsolete("Use CreatedAtUnixTimeSeconds")]
        public int CreatedUnixTime { get { return CreatedAtUnixTimeSeconds; } }

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime; } }

        /// <summary>
        /// The name of the file.
        /// </summary>
        
        [JsonProperty("filename")]
        public string FileName { get; private set; }

        /// <summary>
        /// The intended purpose of the file.
        /// Supported values are 'fine-tune', 'fine-tune-results', 'assistants', and 'assistants_output'.
        /// </summary>
        
        [JsonProperty("purpose")]
        public string Purpose { get; private set; }

        public static implicit operator string(FileData fileData) { return fileData == null ? null : fileData.ToString(); }

        public static implicit operator FileResponse(FileData fileData) { return new FileResponse(fileData); }

        public override string ToString() { return Id; }
    }
}
