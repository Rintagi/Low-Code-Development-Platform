using System.Security.Authentication;
using Newtonsoft.Json;

namespace OpenAI
{
    internal class AuthInfo
    {
        internal const string SecretKeyPrefix = "sk-";
        internal const string SessionKeyPrefix = "sess-";
        internal const string OrganizationPrefix = "org-";

        [JsonConstructor]
        public AuthInfo(string apiKey, string organizationId = null)
        {
            ApiKey = apiKey;

            if (!string.IsNullOrWhiteSpace(organizationId))
            {
                if (!organizationId.Contains(OrganizationPrefix))
                {
                    throw new InvalidCredentialException(string.Format("{0} must start with '{1}'", "organizationId", OrganizationPrefix));
                }

                OrganizationId = organizationId;
            }
        }

        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("organization")]
        public string OrganizationId { get; set; }
    }
}
