using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class RunStepMessageCreation
    {
        /// <summary>
        /// The ID of the message that was created by this run step.
        /// </summary>
        
        [JsonProperty("message_id")]
        public string MessageId { get; private set; }
    }
}