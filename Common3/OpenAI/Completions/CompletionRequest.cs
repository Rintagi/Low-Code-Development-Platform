using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.Completions
{
    /// <summary>
    /// Represents a request to the <see cref="CompletionsEndpoint"/>.  Mostly matches the parameters in
    /// <see href="https://platform.openai.com/docs/api-reference/completions">the OpenAI docs</see>,
    /// although some have been renames or expanded into single/multiple properties for ease of use.
    /// </summary>
    public sealed class CompletionRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        /// <summary>
        /// If you are requesting more than one prompt, specify them as an array of strings.
        /// </summary>
        [JsonProperty("prompt")]
        public string[] Prompts = Array.Empty<string>();

        /// <summary>
        /// For convenience, if you are only requesting a single prompt, set it here
        /// </summary>
        [JsonIgnore]
        public string Prompt
        {
            get { return Prompts != null ? null : Prompts.FirstOrDefault();}
            set
            {
                if (value == null)
                {
                    Prompts = Array.Empty<string>();
                }
                else
                {
                    if (Prompts.Length == 1)
                    {
                        Prompts[0] = value;
                    }
                    else
                    {
                        Prompts = new[] { value };
                    }
                }
            }
        }

        /// <summary>
        /// The suffix that comes after a completion of inserted text.
        /// </summary>
        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        /// <summary>
        /// How many tokens to complete to. Can return fewer if a stop sequence is hit.
        /// </summary>
        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }

        /// <summary>
        /// What sampling temperature to use. Higher values means the model will take more risks.
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
        /// It is generally recommend to use this or <see cref="TopP"/> but not both.
        /// </summary>
        [JsonProperty("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling,
        /// where the model considers the results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// It is generally recommend to use this or <see cref="Temperature"/> but not both.
        /// </summary>
        [JsonProperty("top_p")]
        public double? TopP { get; set; }

        /// <summary>
        /// The scale of the penalty applied if a token is already present at all.
        /// Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.
        /// </summary>
        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; set; }

        /// <summary>
        /// The scale of the penalty for how often a token is used.
        /// Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.
        /// </summary>
        [JsonProperty("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }

        /// <summary>
        /// How many different choices to request for each prompt.
        /// </summary>
        [JsonProperty("n")]
        public int? NumChoicesPerPrompt { get; set; }

        /// <summary>
        /// Specifies where the results should stream and be returned at one time.
        /// Do not set this yourself, use the appropriate methods on <see cref="CompletionsEndpoint"/> instead.
        /// </summary>
        [JsonProperty("stream")]
        public bool Stream { get; internal set; }

        /// <summary>
        /// Include the log probabilities on the most likely tokens, which can be found in
        /// <see cref="CompletionResult.Completions"/> -> <see cref="Choice.LogProbabilities"/>.
        /// So for example, if logprobs is 10, the API will return a list of the 10 most likely tokens.
        /// If logprobs is supplied, the API will always return the logprob of the sampled token,
        /// so there may be up to logprobs+1 elements in the response.
        /// </summary>
        [JsonProperty("logprobs")]
        public int? LogProbabilities { get; set; }

        /// <summary>
        /// Echo back the prompt in addition to the completion
        /// </summary>
        [JsonProperty("echo")]
        public bool? Echo { get; set; }

        /// <summary>
        /// One or more sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
        /// </summary>
        [JsonProperty("stop")]
        public string[] StopSequences { get; set; }

        /// <summary>
        /// The stop sequence where the API will stop generating further tokens. The returned text will not contain the stop sequence.
        /// For convenience, if you are only requesting a single stop sequence, set it here
        /// </summary>
        [JsonIgnore]
        public string StopSequence
        {
            get { return StopSequence == null ? null : StopSequences.FirstOrDefault(); }
            set
            {
                if (value == null)
                {
                    StopSequences = Array.Empty<string>();
                }
                else
                {
                    if (StopSequences.Length == 1)
                    {
                        StopSequences[0] = value;
                    }
                    else
                    {
                        StopSequences = new[] { value };
                    }
                }
            }
        }

        /// <summary>
        /// The logit bias dictionary of token bias values for modifying likelihoods. Positive biases increases the likelihood of
        /// generating a token, while negative biases decreases the probability of a token. Very large biases (e.g. -100,100) can
        /// either eliminate or force a token to be generated.
        /// </summary>
        [JsonProperty("logit_bias")]
        public Dictionary<string, double> LogitBias { get; set; }

        /// <summary>
        /// How many different completions to generate.  Interacts with <see cref="NumChoicesPerPrompt"/> to generate top
        /// NumChoicesPerPrompt out of BestOf. In cases where both are set, BestOf should be greater than NumChoicesPerPrompt.
        /// </summary>
        [JsonProperty("best_of")]
        public int? BestOf { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; }

        /// <summary>
        /// This allows you to set default parameters for every request, for example to set a default temperature or max tokens.
        /// For every request, if you do not have a parameter set on the request but do have it set here as a default,
        /// the request will automatically pick up the default value.
        /// </summary>
        [JsonIgnore]
        public static CompletionRequest DefaultCompletionRequestArgs  = null;

        /// <summary>
        /// Creates a new <see cref="CompletionRequest"/> based on the <see cref="DefaultCompletionRequestArgs"/>.
        /// </summary>
        public CompletionRequest() : this(DefaultCompletionRequestArgs) { }

        /// <summary>
        /// Creates a new <see cref="CompletionRequest"/>, inheriting any parameters set in <paramref name="basedOn"/>.
        /// </summary>
        /// <param name="basedOn">The <see cref="CompletionRequest"/> to copy</param>
        public CompletionRequest(CompletionRequest basedOn)
        {
            if (basedOn == null) { return; }
            Model = basedOn.Model ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.Model) ?? Models.Model.Davinci;
            Prompts = basedOn.Prompts;
            Suffix = basedOn.Suffix ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.Suffix);
            MaxTokens = basedOn.MaxTokens ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.MaxTokens);
            Temperature = basedOn.Temperature ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.Temperature);
            TopP = basedOn.TopP ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.TopP);
            NumChoicesPerPrompt = basedOn.NumChoicesPerPrompt ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.NumChoicesPerPrompt);
            PresencePenalty = basedOn.PresencePenalty ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.PresencePenalty);
            FrequencyPenalty = basedOn.FrequencyPenalty ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.FrequencyPenalty);
            LogProbabilities = basedOn.LogProbabilities ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.LogProbabilities);
            Echo = basedOn.Echo ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.Echo);
            StopSequences = basedOn.StopSequences ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.StopSequences);
            LogitBias = basedOn.LogitBias ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.LogitBias);
            BestOf = basedOn.BestOf ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.BestOf);
            User = basedOn.User ?? (DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.User);
        }

        /// <summary>
        /// Creates a new <see cref="CompletionRequest"/> with the specified parameters
        /// </summary>
        /// <param name="model">ID of the model to use. You can use the List models API to see all of your available models, or see our Model overview for descriptions of them.</param>
        /// <param name="prompt">The prompt to generate from</param>
        /// <param name="prompts">The prompts to generate from</param>
        /// <param name="suffix">The suffix that comes after a completion of inserted text.</param>
        /// <param name="maxTokens">How many tokens to complete to. Can return fewer if a stop sequence is hit.</param>
        /// <param name="temperature">What sampling temperature to use. Higher values means the model will take more risks.
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
        /// It is generally recommend to use this or <paramref name="topP"/> but not both.</param>
        /// <param name="topP">An alternative to sampling with temperature, called nucleus sampling,
        /// where the model considers the results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// It is generally recommend to use this or <paramref name="temperature"/> but not both.</param>
        /// <param name="numOutputs">How many different choices to request for each prompt.</param>
        /// <param name="presencePenalty">The scale of the penalty applied if a token is already present at all.
        /// Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.</param>
        /// <param name="frequencyPenalty">The scale of the penalty for how often a token is used.
        /// Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.</param>
        /// <param name="logProbabilities">Include the log probabilities on the logProbabilities most likely tokens,
        /// which can be found in <see cref="CompletionResult.Completions"/> -> <see cref="Choice.LogProbabilities"/>.
        /// So for example, if logprobs is 10, the API will return a list of the 10 most likely tokens. If logprobs is supplied,
        /// the API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response.</param>
        /// <param name="echo">Echo back the prompt in addition to the completion.</param>
        /// <param name="stopSequences">One or more sequences where the API will stop generating further tokens.
        /// The returned text will not contain the stop sequence.</param>
        /// <param name="logitBias">A dictionary of logit bias to influence the probability of generating a token.</param>
        /// <param name="bestOf">Returns the top bestOf results based on the best probability.</param>
        /// <param name="user">A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.</param>
        public CompletionRequest(
            string model = null,
            string prompt = null,
            IEnumerable<string> prompts = null,
            string suffix = null,
            int? maxTokens = null,
            double? temperature = null,
            double? topP = null,
            int? numOutputs = null,
            double? presencePenalty = null,
            double? frequencyPenalty = null,
            int? logProbabilities = null,
            bool? echo = null,
            IEnumerable<string> stopSequences = null,
            Dictionary<string, double> logitBias = null,
            int? bestOf = null,
            string user = null)
        {
            if (prompt != null)
            {
                Prompt = prompt;
            }
            else if (prompts != null)
            {
                Prompts = prompts.ToArray();
            }
            else
            {
                throw new ArgumentNullException("prompts", "Missing required prompt or prompts");
            }

            Model = string.IsNullOrWhiteSpace(model)
                ? (string.IsNullOrWhiteSpace(DefaultCompletionRequestArgs == null ? null : DefaultCompletionRequestArgs.Model)
                        ? (string) Models.Model.Davinci
                        : DefaultCompletionRequestArgs.Model)
                    : model;
            Suffix = suffix != null ? suffix : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.Suffix : null);
            MaxTokens = maxTokens != null ? maxTokens : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.MaxTokens : null);
            Temperature = temperature != null ? temperature : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.Temperature : null);
            TopP = topP != null ? topP : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.TopP : null);
            NumChoicesPerPrompt = numOutputs != null ? numOutputs : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.NumChoicesPerPrompt : null);
            PresencePenalty = presencePenalty != null ? presencePenalty : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.PresencePenalty : null);
            FrequencyPenalty = frequencyPenalty != null ? frequencyPenalty : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.FrequencyPenalty : null);
            LogProbabilities = logProbabilities != null ? logProbabilities : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.LogProbabilities : null);
            Echo = echo != null ? echo : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.Echo : null);
            StopSequences = stopSequences != null ? stopSequences.ToArray() : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.StopSequences : null);
            LogitBias = logitBias != null ? logitBias : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.LogitBias : null);
            BestOf = bestOf != null ? bestOf : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.BestOf : null);
            User = user != null ? user : (DefaultCompletionRequestArgs != null ? DefaultCompletionRequestArgs.User : null);        }
    }
}
