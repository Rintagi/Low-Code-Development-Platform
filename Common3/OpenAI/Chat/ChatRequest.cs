using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace OpenAI.Chat
{
    public sealed class ChatRequest
    {
        /// <inheritdoc />
        [Obsolete("Use new constructor arguments")]
        public ChatRequest(
            IEnumerable<Message> messages,
            IEnumerable<Function> functions,
            string functionCall = null,
            string model = null,
            double? temperature = null,
            double? topP = null,
            int? number = null,
            string[] stops = null,
            int? maxTokens = null,
            double? presencePenalty = null,
            double? frequencyPenalty = null,
            IReadOnlyDictionary<string, double> logitBias = null,
            string user = null)
            : this(messages, model, frequencyPenalty, logitBias, maxTokens, number, presencePenalty, ChatResponseFormat.Text, maxTokens, stops, temperature, topP, user)
        {
            var functionList = functions == null ? null : functions.ToList();

            if (functionList != null && functionList.Any())
            {
                if (string.IsNullOrWhiteSpace(functionCall))
                {
                    FunctionCall = "auto";
                }
                else
                {
                    if (!functionCall.Equals("none") &&
                        !functionCall.Equals("auto"))
                    {
                        FunctionCall = JToken.FromObject(new { name = functionCall });
                    }
                    else
                    {
                        FunctionCall = functionCall;
                    }
                }
            }

            Functions = functionList == null ? null : functionList.ToList();
        }

        /// <inheritdoc />
        public ChatRequest(
            IEnumerable<Message> messages,
            IEnumerable<Tool> tools,
            string toolChoice = null,
            string model = null,
            double? frequencyPenalty = null,
            IReadOnlyDictionary<string, double> logitBias = null,
            int? maxTokens = null,
            int? number = null,
            double? presencePenalty = null,
            ChatResponseFormat responseFormat = ChatResponseFormat.Text,
            string[] stops = null,
            double? temperature = null,
            double? topP = null,
            string user = null)
            : this(messages, model, frequencyPenalty, logitBias, maxTokens, number, presencePenalty, responseFormat, number, stops, temperature, topP, user)
        {
            var tooList = tools == null ? null : tools.ToList();

            if (tooList != null && tooList.Any())
            {
                if (string.IsNullOrWhiteSpace(toolChoice))
                {
                    ToolChoice = "auto";
                }
                else
                {
                    if (!toolChoice.Equals("none") &&
                        !toolChoice.Equals("auto"))
                    {
                        var tool = JObject.FromObject(new
                        {
                            type = "function",
                            function = JObject.FromObject(new 
                            {
                                name = toolChoice
                            })
                        });
                        ToolChoice = tool;
                    }
                    else
                    {
                        ToolChoice = toolChoice;
                    }
                }
            }

            Tools =tooList == null ? null : tooList.ToList();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messages">
        /// The list of messages for the current chat session.
        /// </param>
        /// <param name="model">
        /// Id of the model to use.
        /// </param>
        /// <param name="temperature">
        /// What sampling temperature to use, between 0 and 2.
        /// Higher values like 0.8 will make the output more random, while lower values like 0.2 will
        /// make it more focused and deterministic.
        /// We generally recommend altering this or top_p but not both.<br/>
        /// Defaults to 1
        /// </param>
        /// <param name="topP">
        /// An alternative to sampling with temperature, called nucleus sampling,
        /// where the model considers the results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both.<br/>
        /// Defaults to 1
        /// </param>
        /// <param name="number">
        /// How many chat completion choices to generate for each input message.<br/>
        /// Defaults to 1
        /// </param>
        /// <param name="seed"></param>
        /// <param name="stops">
        /// Up to 4 sequences where the API will stop generating further tokens.
        /// </param>
        /// <param name="maxTokens">
        /// The maximum number of tokens allowed for the generated answer.
        /// By default, the number of tokens the model can return will be (4096 - prompt tokens).
        /// </param>
        /// <param name="presencePenalty">
        /// Number between -2.0 and 2.0.
        /// Positive values penalize new tokens based on whether they appear in the text so far,
        /// increasing the model's likelihood to talk about new topics.<br/>
        /// Defaults to 0
        /// </param>
        /// <param name="responseFormat">
        /// An object specifying the format that the model must output.
        /// Setting to <see cref="ChatResponseFormat.Json"/> enables JSON mode,
        /// which guarantees the message the model generates is valid JSON.
        /// </param>
        /// <param name="frequencyPenalty">
        /// Number between -2.0 and 2.0.
        /// Positive values penalize new tokens based on their existing frequency in the text so far,
        /// decreasing the model's likelihood to repeat the same line verbatim.<br/>
        /// Defaults to 0
        /// </param>
        /// <param name="logitBias">
        /// Modify the likelihood of specified tokens appearing in the completion.
        /// Accepts a json object that maps tokens(specified by their token ID in the tokenizer)
        /// to an associated bias value from -100 to 100. Mathematically, the bias is added to the logits
        /// generated by the model prior to sampling.The exact effect will vary per model, but values between
        /// -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result
        /// in a ban or exclusive selection of the relevant token.<br/>
        /// Defaults to null
        /// </param>
        /// <param name="user">
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </param>
        public ChatRequest(
            IEnumerable<Message> messages,
            string model = null,
            double? frequencyPenalty = null,
            IReadOnlyDictionary<string, double> logitBias = null,
            int? maxTokens = null,
            int? number = null,
            double? presencePenalty = null,
            ChatResponseFormat responseFormat = ChatResponseFormat.Text,
            int? seed = null,
            string[] stops = null,
            double? temperature = null,
            double? topP = null,
            string user = null)
        {
            Messages = messages == null ? null : messages.ToList();

            if (Messages == null || Messages.Count == 0)
            {
                throw new ArgumentNullException("messages", string.Format("Missing required message parameter"));
            }

            Model = string.IsNullOrWhiteSpace(model) ? (string) Models.Model.GPT3_5_Turbo : model;
            FrequencyPenalty = frequencyPenalty;
            LogitBias = logitBias;
            MaxTokens = maxTokens;
            Number = number;
            PresencePenalty = presencePenalty;
            ResponseFormat = ChatResponseFormat.Json == responseFormat ? responseFormat : ChatResponseFormat.Text;
            Seed = seed;
            Stops = stops;
            Temperature = temperature;
            TopP = topP;
            User = user;
        }

        /// <summary>
        /// The messages to generate chat completions for, in the chat format.
        /// </summary>
        [JsonProperty("messages")]
        public IReadOnlyList<Message> Messages { get; private set; }

        /// <summary>
        /// ID of the model to use.
        /// </summary>
        [JsonProperty("model")]
        public string Model { get; private set; }

        /// <summary>
        /// Number between -2.0 and 2.0.
        /// Positive values penalize new tokens based on their existing frequency in the text so far,
        /// decreasing the model's likelihood to repeat the same line verbatim.<br/>
        /// Defaults to 0
        /// </summary>
        [JsonProperty("frequency_penalty")]
        public double? FrequencyPenalty { get; private set; }

        /// <summary>
        /// Modify the likelihood of specified tokens appearing in the completion.
        /// Accepts a json object that maps tokens(specified by their token ID in the tokenizer)
        /// to an associated bias value from -100 to 100. Mathematically, the bias is added to the logits
        /// generated by the model prior to sampling.The exact effect will vary per model, but values between
        /// -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result
        /// in a ban or exclusive selection of the relevant token.<br/>
        /// Defaults to null
        /// </summary>
        [JsonProperty("logit_bias")]
        public IReadOnlyDictionary<string, double> LogitBias { get; private set; }

        /// <summary>
        /// The maximum number of tokens allowed for the generated answer.
        /// By default, the number of tokens the model can return will be (4096 - prompt tokens).
        /// </summary>
        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; private set; }

        /// <summary>
        /// How many chat completion choices to generate for each input message.<br/>
        /// Defaults to 1
        /// </summary>
        [JsonProperty("n")]
        public int? Number { get; private set; }

        /// <summary>
        /// Number between -2.0 and 2.0.
        /// Positive values penalize new tokens based on whether they appear in the text so far,
        /// increasing the model's likelihood to talk about new topics.<br/>
        /// Defaults to 0
        /// </summary>
        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; private set; }

        /// <summary>
        /// An object specifying the format that the model must output.
        /// Setting to <see cref="ChatResponseFormat.Json"/> enables JSON mode,
        /// which guarantees the message the model generates is valid JSON.
        /// </summary>
        /// <remarks>
        /// Important: When using JSON mode you must still instruct the model to produce JSON yourself via some conversation message,
        /// for example via your system message. If you don't do this, the model may generate an unending stream of
        /// whitespace until the generation reaches the token limit, which may take a lot of time and give the appearance
        /// of a "stuck" request. Also note that the message content may be partial (i.e. cut off) if finish_reason="length",
        /// which indicates the generation exceeded max_tokens or the conversation exceeded the max context length.
        /// </remarks>
        [JsonProperty("response_format")]
        public ResponseFormat ResponseFormat { get; private set; }

        /// <summary>
        /// This feature is in Beta. If specified, our system will make a best effort to sample deterministically,
        /// such that repeated requests with the same seed and parameters should return the same result.
        /// Determinism is not guaranteed, and you should refer to the system_fingerprint response parameter to
        /// monitor changes in the backend.
        /// </summary>
        [JsonProperty("seed")]
        public int? Seed { get; private set; }

        /// <summary>
        /// Up to 4 sequences where the API will stop generating further tokens.
        /// </summary>
        [JsonProperty("stop")]
        public string[] Stops { get; private set; }

        /// <summary>
        /// Specifies where the results should stream and be returned at one time.
        /// Do not set this yourself, use the appropriate methods on <see cref="ChatEndpoint"/> instead.<br/>
        /// Defaults to false
        /// </summary>
        [JsonProperty("stream")]
        public bool Stream { get; internal set; }

        /// <summary>
        /// What sampling temperature to use, between 0 and 2.
        /// Higher values like 0.8 will make the output more random, while lower values like 0.2 will
        /// make it more focused and deterministic.
        /// We generally recommend altering this or top_p but not both.<br/>
        /// Defaults to 1
        /// </summary>
        [JsonProperty("temperature")]
        public double? Temperature { get; private set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling,
        /// where the model considers the results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both.<br/>
        /// Defaults to 1
        /// </summary>
        [JsonProperty("top_p")]
        public double? TopP { get; private set; }

        /// <summary>
        /// A list of tools the model may call. Currently, only functions are supported as a tool.
        /// Use this to provide a list of functions the model may generate JSON inputs for.
        /// </summary>
        [JsonProperty("tools")]
        public IReadOnlyList<Tool> Tools { get; private set; }

        /// <summary>
        /// Controls which (if any) function is called by the model.<br/>
        /// 'none' means the model will not call a function and instead generates a message.&lt;br/&gt;
        /// 'auto' means the model can pick between generating a message or calling a function.&lt;br/&gt;
        /// Specifying a particular function via {"type: "function", "function": {"name": "my_function"}}
        /// forces the model to call that function.<br/>
        /// 'none' is the default when no functions are present.<br/>
        /// 'auto' is the default if functions are present.<br/>
        /// </summary>
        [JsonProperty("tool_choice")]
        public dynamic ToolChoice { get; private set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </summary>
        [JsonProperty("user")]
        public string User { get; private set; }

        /// <summary>
        /// Pass "auto" to let the OpenAI service decide, "none" if none are to be called,
        /// or "functionName" to force function call. Defaults to "auto".
        /// </summary>
        [Obsolete("Use ToolChoice")]
        [JsonProperty("function_call")]
        public dynamic FunctionCall { get; private set; }

        /// <summary>
        /// An optional list of functions to get arguments for.
        /// </summary>
        [Obsolete("Use Tools")]
        [JsonProperty("functions")]
        public IReadOnlyList<Function> Functions { get; private set; }

        public bool ShouldSerializeFunctions()
        {
#pragma warning disable 0618 // Type or member is obsolete
            return Functions != null;
        }

        public bool ShouldSerializeFunctionCall()
        {
            return FunctionCall != null;
        }
#pragma warning restore 0618 // Type or member is obsolete

        public bool ShouldSerializeStream()
        {
            return Stream != false;
        }

        public bool ShouldSerializeResponseFormat()
        {
            return ResponseFormat != null;
        }

        /// <inheritdoc />
        public override string ToString() { return Newtonsoft.Json.JsonConvert.SerializeObject(this, OpenAIClient.jsonSerializationOptions); }
    }
}
