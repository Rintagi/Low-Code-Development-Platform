using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAI.Models
{
    /// <summary>
    /// Represents a language model.<br/>
    /// <see href="https://platform.openai.com/docs/models/model-endpoint-compatability"/>
    /// </summary>
    public sealed class Model
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Model id.</param>
        /// <param name="ownedBy">Optional, owned by id.</param>
        public Model(string id, string ownedBy = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id", "Missing the id of the specified model.");
            }

            Id = id;
            OwnedBy = ownedBy;
        }

        /// <summary>
        /// Allows a model to be implicitly cast to the string of its id.
        /// </summary>
        /// <param name="model">The <see cref="Model"/> to cast to a string.</param>
        public static implicit operator string(Model model) { return model != null ? model.ToString() : null; }

        /// <summary>
        /// Allows a string to be implicitly cast as a <see cref="Model"/>
        /// </summary>
        public static implicit operator Model(string name) { return new Model(name); }

        /// <inheritdoc />
        public override string ToString() { return Id; }

        
        [JsonProperty("id")]
        public string Id { get; private set; }

        
        [JsonProperty("object")]
        public string Object { get; private set; }

        
        [JsonProperty("created")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt { get { return DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime; } }

        
        [JsonProperty("owned_by")]
        public string OwnedBy { get; private set; }

        
        [JsonProperty("permission")]
        public IReadOnlyList<Permission> Permissions { get; private set; }

        
        [JsonProperty("root")]
        public string Root { get; private set; }

        
        [JsonProperty("parent")]
        public string Parent { get; private set; }

        /// <summary>
        /// More capable than any GPT-3.5 model, able to do more complex tasks, and optimized for chat.
        /// Will be updated with our latest model iteration.
        /// </summary>
        public static Model GPT4 = new Model("gpt-4", "openai");

        /// <summary>
        /// Same capabilities as the base gpt-4 mode but with 4x the context length.
        /// Will be updated with our latest model iteration.  Tokens are 2x the price of gpt-4.
        /// </summary>
        public static Model GPT4_32K = new Model("gpt-4-32k", "openai");

        /// <summary>
        /// Because gpt-3.5-turbo performs at a similar capability to text-davinci-003 but at 10%
        /// the price per token, we recommend gpt-3.5-turbo for most use cases.
        /// </summary>
        public static Model GPT3_5_Turbo = new Model("gpt-3.5-turbo", "openai");

        /// <summary>
        /// Same capabilities as the base gpt-3.5-turbo mode but with 4x the context length.
        /// Tokens are 2x the price of gpt-3.5-turbo. Will be updated with our latest model iteration.
        /// </summary>
        public static Model GPT3_5_Turbo_16K = new Model("gpt-3.5-turbo-16k", "openai");

        /// <summary>
        /// The most powerful, largest engine available, although the speed is quite slow.<para/>
        /// Good at: Complex intent, cause and effect, summarization for audience
        /// </summary>
        public static Model Davinci = new Model("text-davinci-003", "openai");

        /// <summary>
        /// For edit requests.
        /// </summary>
        public static Model DavinciEdit = new Model("text-davinci-edit-001", "openai");

        /// <summary>
        /// The 2nd most powerful engine, a bit faster than <see cref="Davinci"/>, and a bit faster.<para/>
        /// Good at: Language translation, complex classification, text sentiment, summarization.
        /// </summary>
        public static Model Curie = new Model("text-curie-001", "openai");

        /// <summary>
        /// The 2nd fastest engine, a bit more powerful than <see cref="Ada"/>, and a bit slower.<para/>
        /// Good at: Moderate classification, semantic search classification
        /// </summary>
        public static Model Babbage = new Model("text-babbage-001", "openai");

        /// <summary>
        /// The smallest, fastest engine available, although the quality of results may be poor.<para/>
        /// Good at: Parsing text, simple classification, address correction, keywords
        /// </summary>
        public static Model Ada = new Model("text-ada-001", "openai");

        /// <summary>
        /// The default model for <see cref="Embeddings.EmbeddingsEndpoint"/>.
        /// </summary>
        public static Model Embedding_Ada_002  = new Model("text-embedding-ada-002", "openai");

        /// <summary>
        /// The default model for <see cref="Audio.AudioEndpoint"/>.
        /// </summary>
        public static Model Whisper1  = new Model("whisper-1", "openai");

        /// <summary>
        /// The default model for <see cref="Moderations.ModerationsEndpoint"/>.
        /// </summary>
        public static Model Moderation_Latest  = new Model("text-moderation-latest", "openai");

        /// <summary>
        /// The default model for <see cref="Audio.SpeechRequest"/>s.
        /// </summary>
        public static Model TTS_1  = new Model("tts-1", "openai");

        public static Model TTS_1HD  = new Model("tts-1-hd", "openai");

        /// <summary>
        /// The default model for <see cref="Images.ImagesEndpoint"/>.
        /// </summary>
        public static Model DallE_2  = new Model("dall-e-2", "openai");

        public static Model DallE_3  = new Model("dall-e-3", "openai");
    }
}
