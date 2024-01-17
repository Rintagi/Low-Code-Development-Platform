using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class RequiredAction
    {
        
        [JsonProperty("type")]
        public string Type { get; private set; }

        /// <summary>
        /// Details on the tool outputs needed for this run to continue.
        /// </summary>
        
        [JsonProperty("submit_tool_outputs")]
        public SubmitToolOutputs SubmitToolOutputs { get; private set; }
    }
}