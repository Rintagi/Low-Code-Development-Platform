using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Threads
{
    public sealed class TextContent
    {
        /// <summary>
        /// The data that makes up the text.
        /// </summary>
        
        [JsonProperty("value")]
        public string Value { get; private set; }

        /// <summary>
        /// Annotations
        /// </summary>
        
        [JsonProperty("annotations")]
        public IReadOnlyList<Annotation> Annotations { get; private set; }

        public bool ShouldSerializeAnnotations()
        {
            return Annotations != null;
        }

    }
}