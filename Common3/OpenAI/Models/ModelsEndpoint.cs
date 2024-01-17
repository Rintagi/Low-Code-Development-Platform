using OpenAI.Extensions;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Models
{
    /// <summary>
    /// List and describe the various models available in the API.
    /// You can refer to the Models documentation to understand what <see href="https://platform.openai.com/docs/models"/> are available and the differences between them.<br/>
    /// <see href="https://platform.openai.com/docs/api-reference/models"/>
    /// </summary>
    public sealed class ModelsEndpoint : BaseEndPoint
    {
        private sealed class ModelsList
        {
            [JsonProperty("data")]
            public List<Model> Models { get; private set; }
        }

        /// <inheritdoc />
        public ModelsEndpoint(OpenAIClient client) : base(client) { }

        /// <inheritdoc />
        protected override string Root { get { return "models"; } }

        /// <summary>
        /// List all models via the API
        /// </summary>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>Asynchronously returns the list of all <see cref="Model"/>s</returns>
        public async Task<IReadOnlyList<Model>> GetModelsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.GetAsync(GetUrl(), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ModelsList>(responseAsString, OpenAIClient.jsonSerializationOptions).Models;
        }

        /// <summary>
        /// Get the details about a particular Model from the API
        /// </summary>
        /// <param name="id">The id/name of the model to get more details about</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>Asynchronously returns the <see cref="Model"/> with all available properties</returns>
        public async Task<Model> GetModelDetailsAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.GetAsync(GetUrl(string.Format("/{0}",id)), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Model>(responseAsString, OpenAIClient.jsonSerializationOptions);
        }

        /// <summary>
        /// Delete a fine-tuned model. You must have the Owner role in your organization.
        /// </summary>
        /// <param name="modelId">The <see cref="Model"/> to delete.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>True, if fine-tuned model was successfully deleted.</returns>
        public async Task<bool> DeleteFineTuneModelAsync(string modelId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = await GetModelDetailsAsync(modelId, cancellationToken).ConfigureAwait(false);

            if (model == null)
            {
                throw new Exception(string.Format("Failed to get {0} info!",modelId));
            }

            // Don't check ownership as API does it for us.

            try
            {
                var response = await client.client.DeleteAsync(GetUrl(string.Format("/{0}",model.Id)), cancellationToken).ConfigureAwait(false);
                var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<DeletedResponse>(responseAsString, OpenAIClient.jsonSerializationOptions).Deleted;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("You have insufficient permissions for this operation. You need to be this role: Owner."))
                {
                    throw new UnauthorizedAccessException(string.Format("You have insufficient permissions for this operation. You need to be this role: Owner.\n{0}", e));
                }

                throw;
            }
        }
    }
}
