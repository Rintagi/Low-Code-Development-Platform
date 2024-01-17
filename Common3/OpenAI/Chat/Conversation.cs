using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Chat
{
    public sealed class Conversation
    {
        [JsonConstructor]
        public Conversation(List<Message> messages)
        {
            this.messages = messages;
        }

        private readonly List<Message> messages;

        [JsonProperty("messages")]
        public IReadOnlyList<Message> Messages { get { return messages; }}

        /// <summary>
        /// Appends <see cref="Message"/> to the end of <see cref="Messages"/>.
        /// </summary>
        /// <param name="message">The message to add to the <see cref="Conversation"/>.</param>
        public void AppendMessage(Message message) { messages.Add(message);}

        public override string ToString() { return Newtonsoft.Json.JsonConvert.SerializeObject(this, OpenAIClient.jsonSerializationOptions); }

        public static implicit operator string(Conversation conversation) { return conversation == null ? null : conversation.ToString(); }
    }
}