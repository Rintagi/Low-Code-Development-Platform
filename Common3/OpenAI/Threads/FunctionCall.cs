using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class FunctionCall
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The arguments that the model expects you to pass to the function.
        /// </summary>
        
        [JsonProperty("arguments")]
        public string Arguments { get; private set; }
    }
}