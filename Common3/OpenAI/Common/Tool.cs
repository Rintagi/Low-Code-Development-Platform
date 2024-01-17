using Newtonsoft.Json;

namespace OpenAI
{
    public sealed class Tool
    {
        public Tool() { }

        public Tool(Tool other) { CopyFrom(other); }

        public Tool(Function function)
        {
            Function = function;
            Type = "function";
        }

        public static implicit operator Tool(Function function) { return new Tool(function); }

        public static Tool Retrieval  = new Tool { Type = "retrieval" };

        public static Tool CodeInterpreter = new Tool { Type = "code_interpreter" };

        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("index")]
        public int? Index { get; private set; }

        
        [JsonProperty("type")]
        public string Type { get; private set; }

        
        [JsonProperty("function")]
        public Function Function { get; private set; }

        internal void CopyFrom(Tool other)
        {
            if (other != null && !string.IsNullOrWhiteSpace(other.Id))
            {
                Id = other.Id;
            }

            if (other != null && other.Index != null)
            {
                Index = other.Index.Value;
            }

            if (other != null && !string.IsNullOrWhiteSpace(other.Type))
            {
                Type = other.Type;
            }

            if (other != null && other.Function != null)
            {
                if (Function == null)
                {
                    Function = new Function(other.Function);
                }
                else
                {
                    Function.CopyFrom(other.Function);
                }
            }
        }
    }
}