using Newtonsoft.Json;

namespace OpenAI
{
    public sealed class Error
    {
        /// <summary>
        /// One of server_error or rate_limit_exceeded.
        /// </summary>
        
        [JsonProperty("code")]
        public string Code { get; private set; }

        /// <summary>
        /// A human-readable description of the error.
        /// </summary>
        
        [JsonProperty("message")]
        public string Message { get; private set; }
    }
}