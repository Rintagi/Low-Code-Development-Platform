using OpenAI.Extensions;
using OpenAI.Files;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Assistants
{
    public sealed class AssistantsEndpoint : BaseEndPoint
    {
        internal AssistantsEndpoint(OpenAIClient client) : base(client) { }

        protected override string Root { get { return "assistants"; } }

        /// <summary>
        /// Get list of assistants.
        /// </summary>
        /// <param name="query"><see cref="ListQuery"/>.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="ListResponse{Assistant}"/></returns>
        public async Task<ListResponse<AssistantResponse>> ListAssistantsAsync(ListQuery query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.GetAsync(GetUrl(queryParameters: query), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<ListResponse<AssistantResponse>>(responseAsString, client);
        }

        /// <summary>
        /// Create an assistant.
        /// </summary>
        /// <param name="request"><see cref="CreateAssistantRequest"/>.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="AssistantResponse"/>.</returns>
        public async Task<AssistantResponse> CreateAssistantAsync(CreateAssistantRequest request = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            request =  request ?? new CreateAssistantRequest();
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(request, OpenAIClient.jsonSerializationOptions).ToJsonStringContent(EnableDebug);
            var response = await client.client.PostAsync(GetUrl(), jsonContent, cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<AssistantResponse>(responseAsString, client);
        }

        /// <summary>
        /// Retrieves an assistant.
        /// </summary>
        /// <param name="assistantId">The ID of the assistant to retrieve.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="AssistantResponse"/>.</returns>
        public async Task<AssistantResponse> RetrieveAssistantAsync(string assistantId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.GetAsync(GetUrl(string.Format("/{0}", assistantId)), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<AssistantResponse>(responseAsString, client);
        }

        /// <summary>
        /// Modifies an assistant.
        /// </summary>
        /// <param name="assistantId">The ID of the assistant to modify.</param>
        /// <param name="request"><see cref="CreateAssistantRequest"/>.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="AssistantResponse"/>.</returns>
        public async Task<AssistantResponse> ModifyAssistantAsync(string assistantId, CreateAssistantRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(request, OpenAIClient.jsonSerializationOptions).ToJsonStringContent(EnableDebug);
            var response = await client.client.PostAsync(GetUrl(string.Format("/{0}", assistantId)), jsonContent, cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<AssistantResponse>(responseAsString, client);
        }

        /// <summary>
        /// Delete an assistant.
        /// </summary>
        /// <param name="assistantId">The ID of the assistant to delete.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns>True, if the assistant was deleted.</returns>
        public async Task<bool> DeleteAssistantAsync(string assistantId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.DeleteAsync(GetUrl(string.Format("/{0}", assistantId)), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DeletedResponse>(responseAsString, OpenAIClient.jsonSerializationOptions).Deleted;
        }

        #region Files

        /// <summary>
        /// Returns a list of assistant files.
        /// </summary>
        /// <param name="assistantId">The ID of the assistant the file belongs to.</param>
        /// <param name="query"><see cref="ListQuery"/>.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="ListResponse{AssistantFile}"/>.</returns>
        public async Task<ListResponse<AssistantFileResponse>> ListFilesAsync(string assistantId, ListQuery query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.GetAsync(GetUrl(string.Format("/{0}/files", assistantId), query), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<ListResponse<AssistantFileResponse>>(responseAsString, client);
        }

        /// <summary>
        /// Attach a file to an assistant.
        /// </summary>
        /// <param name="assistantId"> The ID of the assistant for which to attach a file. </param>
        /// <param name="file">
        /// A <see cref="FileResponse"/> (with purpose="assistants") that the assistant should use.
        /// Useful for tools like retrieval and code_interpreter that can access files.
        /// </param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="AssistantFileResponse"/>.</returns>
        public async Task<AssistantFileResponse> AttachFileAsync(string assistantId, FileResponse file, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (file != null && file.Purpose != null && file.Purpose.Equals("assistants") != true)
            {
                throw new InvalidOperationException(string.Format("file.Purpose must be 'assistants'!"));
            }

            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(new { file_id = file.Id }, OpenAIClient.jsonSerializationOptions).ToJsonStringContent(EnableDebug);
            var response = await client.client.PostAsync(GetUrl(string.Format("/{0}/files",assistantId)), jsonContent, cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<AssistantFileResponse>(responseAsString, client);
        }

        /// <summary>
        /// Retrieves an AssistantFile.
        /// </summary>
        /// <param name="assistantId">The ID of the assistant who the file belongs to.</param>
        /// <param name="fileId">The ID of the file we're getting.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="AssistantFileResponse"/>.</returns>
        public async Task<AssistantFileResponse> RetrieveFileAsync(string assistantId, string fileId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.GetAsync(GetUrl(string.Format("/{0}/files/{1}",assistantId, fileId)), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<AssistantFileResponse>(responseAsString, client);
        }

        /// <summary>
        /// Remove an assistant file.
        /// </summary>
        /// <remarks>
        /// Note that removing an AssistantFile does not delete the original File object,
        /// it simply removes the association between that File and the Assistant.
        /// To delete a File, use the File delete endpoint instead.
        /// </remarks>
        /// <param name="assistantId">The ID of the assistant that the file belongs to.</param>
        /// <param name="fileId">The ID of the file to delete.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns>True, if file was removed.</returns>
        public async Task<bool> RemoveFileAsync(string assistantId, string fileId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.client.DeleteAsync(GetUrl(string.Format("/{0}/files/{1}",assistantId, fileId)), cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DeletedResponse>(responseAsString, OpenAIClient.jsonSerializationOptions).Deleted;
        }

        #endregion Files
    }
}