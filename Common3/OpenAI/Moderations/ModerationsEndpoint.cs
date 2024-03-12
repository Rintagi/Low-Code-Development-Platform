using OpenAI.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Moderations
{
    /// <summary>
    /// The moderation endpoint is a tool you can use to check whether content complies with OpenAI's content policy.
    /// Developers can thus identify content that our content policy prohibits and take action, for instance by filtering it.<br/>
    /// <see href="https://platform.openai.com/docs/api-reference/moderations"/>
    /// </summary>
    public sealed class ModerationsEndpoint : BaseEndPoint
    {
        /// <inheritdoc />
        public ModerationsEndpoint(OpenAIClient client) : base(client) { }

        /// <inheritdoc />
        protected override string Root { get { return "moderations"; } }

        /// <summary>
        /// Classifies if text violates OpenAI's Content Policy.
        /// </summary>
        /// <param name="input">
        /// The input text to classify.
        /// </param>
        /// <param name="model">The default is text-moderation-latest which will be automatically upgraded over time.
        /// This ensures you are always using our most accurate model.
        /// If you use text-moderation-stable, we will provide advanced notice before updating the model.
        /// Accuracy of text-moderation-stable may be slightly lower than for text-moderation-latest.
        /// </param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// True, if the text has been flagged by the model as violating OpenAI's content policy.
        /// </returns>
        public async Task<bool> GetModerationAsync(string input, string model = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await CreateModerationAsync(new ModerationsRequest(input, model), cancellationToken).ConfigureAwait(false);
            return result != null && result.Results != null && result.Results.Count > 0 && result.Results.Any(moderationResult => moderationResult.Flagged);
        }

        /// <summary>
        /// Classifies if text violates OpenAI's Content Policy
        /// </summary>
        /// <param name="request"><see cref="ModerationsRequest"/></param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        public async Task<ModerationsResponse> CreateModerationAsync(ModerationsRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(request, OpenAIClient.jsonSerializationOptions).ToJsonStringContent(EnableDebug);
            var response = await client.client.PostAsync(GetUrl(), jsonContent, cancellationToken).ConfigureAwait(false);
            var responseAsString = await response.ReadAsStringAsync(EnableDebug, cancellationToken).ConfigureAwait(false);
            return response.Deserialize<ModerationsResponse>(responseAsString, client);
        }

        /// <summary>
        /// Classifies if text violates OpenAI's Content Policy.
        /// </summary>
        /// <remarks>
        /// This version splits <paramref name="input"/> into chunks and makes multiple moderation requests,
        /// which should provide better results when dealing with a large <paramref name="input"/>.
        /// <br/><br/> On the first flagged chunk, the method returns.
        /// </remarks>
        /// <param name="input">
        /// The input text to classify.
        /// </param>
        /// <param name="model">The default is text-moderation-latest which will be automatically upgraded over time.
        /// This ensures you are always using our most accurate model.
        /// If you use text-moderation-stable, we will provide advanced notice before updating the model.
        /// Accuracy of text-moderation-stable may be slightly lower than for text-moderation-latest.
        /// </param>
        /// <param name="chunkSize">Maximum size each chunk can be.</param>
        /// <param name="chunkOverlap">How many characters a chunk should contain from the previous chunk.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns>
        /// True, if the text has been flagged by the model as violating OpenAI's content policy.
        /// </returns>
        public async Task<bool> GetModerationChunkedAsync(
            string input,
            string model = null,
            int chunkSize = 1000,
            int chunkOverlap = 100,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException(string.Format("{0} must be greater than 0","chunkSize"));
            }

            if (chunkOverlap <= 0)
            {
                throw new ArgumentException(string.Format("{0} must be greater than 0","chunkOverlap"));
            }

            if (chunkOverlap >= chunkSize)
            {
                throw new ArgumentException(string.Format("{0} must be smaller than {1}","chunkOverlap","chunkSize"));
            }

            for (int i = 0; i < input.Length; i += chunkSize - chunkOverlap)
            {
                var chunk =  input.Substring(i, i + chunkSize > input.Length ? input.Length - i : chunkSize);
                var result = await GetModerationAsync(chunk, model, cancellationToken);

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}