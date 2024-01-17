using OpenAI.Assistants;
using OpenAI.Audio;
using OpenAI.Chat;
using OpenAI.Completions;
using OpenAI.Edits;
using OpenAI.Embeddings;
using OpenAI.Extensions;
using OpenAI.Files;
using OpenAI.FineTuning;
using OpenAI.Images;
using OpenAI.Models;
using OpenAI.Moderations;
using OpenAI.Threads;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenAI
{
    /// <summary>
    /// Entry point to the OpenAI API, handling auth and allowing access to the various API endpoints
    /// </summary>
    public sealed class OpenAIClient
    {
        /// <summary>
        /// Creates a new entry point to the OpenAPI API, handling auth and allowing access to the various API endpoints
        /// </summary>
        /// <param name="openAIAuthentication">
        /// The API authentication information to use for API calls,
        /// or <see langword="null"/> to attempt to use the <see cref="OpenAI.OpenAIAuthentication.Default"/>,
        /// potentially loading from environment vars or from a config file.
        /// </param>
        /// <param name="clientSettings">
        /// Optional, <see cref="OpenAIClientSettings"/> for specifying OpenAI deployments to Azure or proxy domain.
        /// </param>
        /// <param name="client">A <see cref="HttpClient"/>.</param>
        /// <exception cref="AuthenticationException">Raised when authentication details are missing or invalid.</exception>
        public OpenAIClient(OpenAIAuthentication openAIAuthentication = null, OpenAIClientSettings clientSettings = null, HttpClient client = null)
        {
            openAIAuthentication = openAIAuthentication ?? OpenAIAuthentication.Default;
            openAIClientSettings = clientSettings ?? OpenAIClientSettings.Default;

            if (openAIAuthentication == null || openAIAuthentication.ApiKey == null)
            {
                throw new AuthenticationException("You must provide API authentication.  Please refer to https://github.com/RageAgainstThePixel/OpenAI-DotNet#authentication for details.");
            }

            client = SetupClient(client);
            modelsEndpoint = new ModelsEndpoint(this);
            completionsEndpoint = new CompletionsEndpoint(this);
            chatEndpoint = new ChatEndpoint(this);
#pragma warning disable 0618 // Type or member is obsolete
            editsEndpoint = new EditsEndpoint(this);
#pragma warning restore 0618 // Type or member is obsolete
            imagesEndPoint = new ImagesEndpoint(this);
            embeddingsEndpoint = new EmbeddingsEndpoint(this);
            audioEndpoint = new AudioEndpoint(this);
            filesEndpoint = new FilesEndpoint(this);
            fineTuningEndpoint = new FineTuningEndpoint(this);
            moderationsEndpoint = new ModerationsEndpoint(this);
            threadsEndpoint = new ThreadsEndpoint(this);
            assistantsEndpoint = new AssistantsEndpoint(this);
        }

        private HttpClient SetupClient(HttpClient client = null)
        {
            client = client ?? MyHttpClient;
            client.DefaultRequestHeaders.Add("User-Agent", "OpenAI-DotNet");
            client.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v1");

            if (!openAIClientSettings.baseRequestUrlFormat.Contains(OpenAIClientSettings.AzureOpenAIDomain) &&
                (string.IsNullOrWhiteSpace(openAIAuthentication.ApiKey) ||
                 (!openAIAuthentication.ApiKey.Contains(AuthInfo.SecretKeyPrefix) &&
                  !openAIAuthentication.ApiKey.Contains(AuthInfo.SessionKeyPrefix))))
            {
                throw new InvalidCredentialException(string.Format("{0} must start with '{1}'", openAIAuthentication.ApiKey, AuthInfo.SecretKeyPrefix));
            }

            if (openAIClientSettings.useOAuthAuthentication)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAIAuthentication.ApiKey);
            }
            else
            {
                client.DefaultRequestHeaders.Add("api-key", openAIAuthentication.ApiKey);
            }

            if (!string.IsNullOrWhiteSpace(openAIAuthentication.OrganizationId))
            {
                client.DefaultRequestHeaders.Add("OpenAI-Organization", openAIAuthentication.OrganizationId);
            }

            return client;
        }

        internal static HttpClient MyHttpClient = new HttpClient(new HttpClientHandler());

        /// <summary>
        /// <see cref="HttpClient"/> to use when making calls to the API.
        /// </summary>
        internal HttpClient client { get; private set; }

        /// <summary>
        /// The <see cref="JsonSerializationOptions"/> to use when making calls to the API.
        /// </summary>
        public static JsonSerializerSettings jsonSerializationOptions { get; set; }

        /// <summary>
        /// The API authentication information to use for API calls
        /// </summary>
        public OpenAIAuthentication openAIAuthentication { get; set; }

        /// <summary>
        /// The client settings for configuring Azure OpenAI or custom domain.
        /// </summary>
        internal OpenAIClientSettings openAIClientSettings { get; set; }

        /// <summary>
        /// Enables or disables debugging for all endpoints.
        /// </summary>
        public bool EnableDebug { get; set; }

        /// <summary>
        /// List and describe the various models available in the API.
        /// You can refer to the Models documentation to understand what <see href="https://platform.openai.com/docs/models"/> are available and the differences between them.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/models"/>
        /// </summary>
        public ModelsEndpoint modelsEndpoint { get; set; }

        /// <summary>
        /// Text generation is the core function of the API. You give the API a prompt, and it generates a completion.
        /// The way you “program” the API to do a task is by simply describing the task in plain english or providing
        /// a few written examples. This simple approach works for a wide range of use cases, including summarization,
        /// translation, grammar correction, question answering, chatbots, composing emails, and much more
        /// (see the prompt library for inspiration).<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions"/>
        /// </summary>
        public CompletionsEndpoint completionsEndpoint { get; set; }

        /// <summary>
        /// Given a chat conversation, the model will return a chat completion response.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/chat"/>
        /// </summary>
        public ChatEndpoint chatEndpoint { get; set; }

        /// <summary>
        /// Given a prompt and an instruction, the model will return an edited version of the prompt.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/edits"/>
        /// </summary>
        [Obsolete("Deprecated")]
        public EditsEndpoint editsEndpoint { get; set; }

        /// <summary>
        /// Given a prompt and/or an input image, the model will generate a new image.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images"/>
        /// </summary>
        public ImagesEndpoint imagesEndPoint { get; set; }

        /// <summary>
        /// Get a vector representation of a given input that can be easily consumed by machine learning models and algorithms.<br/>
        /// <see href="https://platform.openai.com/docs/guides/embeddings"/>
        /// </summary>
        public EmbeddingsEndpoint embeddingsEndpoint { get; set; }

        /// <summary>
        /// Transforms audio into text.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/audio"/>
        /// </summary>
        public AudioEndpoint audioEndpoint { get; set; }

        /// <summary>
        /// Files are used to upload documents that can be used with features like Fine-tuning.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/files"/>
        /// </summary>
        public FilesEndpoint filesEndpoint { get; set; }

        /// <summary>
        /// Manage fine-tuning jobs to tailor a model to your specific training data.<br/>
        /// <see href="https://platform.openai.com/docs/guides/fine-tuning"/><br/>
        /// <see href="https://platform.openai.com/docs/api-reference/fine-tuning"/>
        /// </summary>
        public FineTuningEndpoint fineTuningEndpoint { get; set; }

        /// <summary>
        /// The moderation endpoint is a tool you can use to check whether content complies with OpenAI's content policy.
        /// Developers can thus identify content that our content policy prohibits and take action, for instance by filtering it.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/moderations"/>
        /// </summary>
        public ModerationsEndpoint moderationsEndpoint { get; set; }

        /// <summary>
        /// Build assistants that can call models and use tools to perform tasks.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/assistants"/>
        /// </summary>
        public AssistantsEndpoint assistantsEndpoint { get; set; }

        /// <summary>
        /// Create threads that assistants can interact with.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/threads"/>
        /// </summary>
        public ThreadsEndpoint threadsEndpoint { get; set; }
    }
}