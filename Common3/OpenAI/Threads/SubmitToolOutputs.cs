using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class SubmitToolOutputs
    {
        /// <summary>
        /// A list of the relevant tool calls.
        /// </summary>
        
        [JsonProperty("tool_calls")]
        public IReadOnlyList<ToolCall> ToolCalls { get; private set; }
    }
}