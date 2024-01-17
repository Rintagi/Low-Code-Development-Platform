using OpenAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OpenAI.Embeddings
{
    public sealed class EmbeddingsRequest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">
        /// Input text to get embeddings for, encoded as a string or array of tokens.
        /// To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays.
        /// Each input must not exceed 8192 tokens in length.
        /// </param>
        /// <param name="model">
        /// ID of the model to use.<br/>
        /// Defaults to: <see cref="Models.Model.Embedding_Ada_002"/>
        /// </param>
        /// <param name="user">
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </param>
        /// <exception cref="ArgumentNullException">A valid <see cref="input"/> string is a Required parameter.</exception>
        public EmbeddingsRequest(string input, string model = null, string user = null)
            : this(new List<string> { input }, model, user)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException("input");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">
        /// Input text to get embeddings for, encoded as a string or array of tokens.
        /// To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays.
        /// Each input must not exceed 8192 tokens in length.
        /// </param>
        /// <param name="model">
        /// The model id to use.
        /// Defaults to: <see cref="Model.Embedding_Ada_002"/>
        /// </param>
        /// <param name="user">
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </param>
        /// <exception cref="ArgumentNullException">A valid <see cref="input"/> string is a Required parameter.</exception>
        public EmbeddingsRequest(IEnumerable<string> input, string model = null, string user = null)
        {
            Input = input.ToList();

            if (Input.Count == 0)
            {
                throw new ArgumentNullException("input", string.Format("Missing required {0} parameter","input"));
            }

            Model = string.IsNullOrWhiteSpace(model) ? (string) Models.Model.Embedding_Ada_002 : model;
            User = user;
        }

        [JsonProperty("input")]
        public IReadOnlyList<string> Input { get; set; }

        [JsonProperty("model")]
        public string Model { get; private set; }

        [JsonProperty("user")]
        public string User { get; private set; }
    }
}
