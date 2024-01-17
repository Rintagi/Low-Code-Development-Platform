using OpenAI.Extensions;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class CodeInterpreterOutputs
    {
        /// <summary>
        /// Output type
        /// </summary>
        
        [JsonProperty("type")]
        [JsonConverter(typeof(JsonStringEnumConverter<CodeInterpreterOutputType>))]
        public CodeInterpreterOutputType Type { get; private set; }

        /// <summary>
        /// The text output from the Code Interpreter tool call.
        /// </summary>
        
        [JsonProperty("logs")]
        public string Logs { get; private set; }

        /// <summary>
        /// Code interpreter image output
        /// </summary>
        
        [JsonProperty("image")]
        public CodeInterpreterImageOutput Image { get; private set; }
    }
}