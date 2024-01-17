using System;
using Newtonsoft.Json;

namespace OpenAI.Edits
{
    [Obsolete("Deprecated")]
    public sealed class Choice
    {
        
        [JsonProperty("text")]
        public string Text { get; private set; }

        
        [JsonProperty("index")]
        public int Index { get; private set; }

        /// <summary>
        /// Gets the main text of this completion
        /// </summary>
        public override string ToString() { return Text; }

        public static implicit operator string(Choice choice) { return choice.Text; }
    }
}
