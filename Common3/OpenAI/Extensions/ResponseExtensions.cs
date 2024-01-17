using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Extensions
{
    internal static class ResponseExtensions
    {
        private const string RequestId = "X-Request-ID";
        private const string Organization = "Openai-Organization";
        private const string ProcessingTime = "Openai-Processing-Ms";
        private const string OpenAIVersion = "openai-version";
        private const string XRateLimitLimitRequests = "x-ratelimit-limit-requests";
        private const string XRateLimitLimitTokens = "x-ratelimit-limit-tokens";
        private const string XRateLimitRemainingRequests = "x-ratelimit-remaining-requests";
        private const string XRateLimitRemainingTokens = "x-ratelimit-remaining-tokens";
        private const string XRateLimitResetRequests = "x-ratelimit-reset-requests";
        private const string XRateLimitResetTokens = "x-ratelimit-reset-tokens";

        private static readonly NumberFormatInfo numberFormatInfo = new NumberFormatInfo
        {
            NumberGroupSeparator = ",",
            NumberDecimalSeparator = "."
        };

        internal static void SetResponseData(this BaseResponse response, HttpResponseHeaders headers, OpenAIClient client)
        {
            if (response is IListResponse<BaseResponse>)
            {
                IListResponse<BaseResponse> listResponse = (IListResponse<BaseResponse>) response;
                foreach (var item in listResponse.Items)
                {
                    SetResponseData(item, headers, client);
                }
            }

            response.Client = client;
            IEnumerable<string> requestId;
            IEnumerable<string> organization;
            IEnumerable<string> processingTimeString;
            IEnumerable<string> version;
            IEnumerable<string> limitRequests;
            IEnumerable<string> limitTokens;
            IEnumerable<string> remainingRequests;
            IEnumerable<string> remainingTokens;
            IEnumerable<string> resetRequests;
            IEnumerable<string> resetTokens;
            double processingTime;
            int limitRequestsValue;
            int limitTokensValue;
            int remainingRequestsValue;
            int remainingTokensValue;

            if (headers == null) { return; }

            if (headers.TryGetValues(RequestId, out requestId))
            {
                response.RequestId = requestId.First();
            }

            if (headers.TryGetValues(Organization, out organization))
            {
                response.Organization = organization.First();
            }

            if (headers.TryGetValues(ProcessingTime, out processingTimeString) &&
                double.TryParse(processingTimeString.First(), NumberStyles.AllowDecimalPoint, numberFormatInfo, out processingTime))
            {
                response.ProcessingTime = TimeSpan.FromMilliseconds(processingTime);
            }

            if (headers.TryGetValues(OpenAIVersion, out version))
            {
                response.OpenAIVersion = version.First();
            }

            if (headers.TryGetValues(XRateLimitLimitRequests, out limitRequests) &&
                int.TryParse(limitRequests.FirstOrDefault(), out limitRequestsValue)
               )
            {
                response.LimitRequests = limitRequestsValue;
            }

            if (headers.TryGetValues(XRateLimitLimitTokens, out limitTokens) &&
                int.TryParse(limitTokens.FirstOrDefault(), out limitTokensValue))
            {
                response.LimitTokens = limitTokensValue;
            }

            if (headers.TryGetValues(XRateLimitRemainingRequests, out remainingRequests) &&
                int.TryParse(remainingRequests.FirstOrDefault(), out remainingRequestsValue))
            {
                response.RemainingRequests = remainingRequestsValue;
            }

            if (headers.TryGetValues(XRateLimitRemainingTokens, out remainingTokens) &&
                int.TryParse(remainingTokens.FirstOrDefault(), out remainingTokensValue))
            {
                response.RemainingTokens = remainingTokensValue;
            }

            if (headers.TryGetValues(XRateLimitResetRequests, out resetRequests))
            {
                response.ResetRequests = resetRequests.FirstOrDefault();
            }

            if (headers.TryGetValues(XRateLimitResetTokens, out resetTokens))
            {
                response.ResetTokens = resetTokens.FirstOrDefault();
            }
        }

        internal static async Task<string> ReadAsStringAsync(this HttpResponseMessage response, bool debugResponse = false, CancellationToken cancellationToken = default(CancellationToken), [CallerMemberName] string methodName = null)
        {
            var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(string.Format("{0} Failed! HTTP status code: {1} | Response body: {2}",methodName, response.StatusCode,responseAsString));
            }

            if (debugResponse)
            {
                Console.WriteLine(responseAsString);
            }

            return responseAsString;
        }

        internal static async Task CheckResponseAsync(this HttpResponseMessage response, CancellationToken cancellationToken = default(CancellationToken), [CallerMemberName] string methodName = null)
        {
            if (!response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new HttpRequestException(message: string.Format("{0} Failed! HTTP status code: {1} | Response body: {2}", methodName, response.StatusCode, responseAsString));
            }
        }

        internal static T Deserialize<T>(this HttpResponseMessage response, string json, OpenAIClient client) where T : BaseResponse
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, OpenAIClient.jsonSerializationOptions);
            result.SetResponseData(response.Headers, client);
            return result;
        }
    }
}
