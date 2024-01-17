using System;
using System.Collections.Generic;

namespace OpenAI
{
    /// <summary>
    /// The client settings for configuring Azure OpenAI or custom domain.
    /// </summary>
    public sealed class OpenAIClientSettings
    {
        internal const string OpenAIDomain = "api.openai.com";
        internal const string DefaultOpenAIApiVersion = "v1";
        public const string AzureOpenAIDomain = "openai.azure.com";
        internal const string DefaultAzureApiVersion = "2022-12-01";

        /// <summary>
        /// Creates a new instance of <see cref="OpenAIClientSettings"/> for use with OpenAI.
        /// </summary>
        public OpenAIClientSettings()
        {
            resourceName = OpenAIDomain;
            apiVersion = "v1";
            deploymentId = string.Empty;
            baseRequest = string.Format("/{0}/", apiVersion);
            baseRequestUrlFormat = string.Format("https://{0}{1}{{0}}", resourceName, baseRequest);
            useOAuthAuthentication = true;
        }

        /// <summary>
        /// Creates a new instance of <see cref="OpenAIClientSettings"/> for use with OpenAI.
        /// </summary>
        /// <param name="domain">Base api domain.</param>
        /// <param name="_apiVersion">The version of the OpenAI api you want to use.</param>
        public OpenAIClientSettings(string domain, string _apiVersion = DefaultOpenAIApiVersion)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = OpenAIDomain;
            }

            if (!domain.Contains(".") &&
                !domain.Contains(":"))
            {
                throw new ArgumentException(string.Format("You're attempting to pass a \"resourceName\" parameter to \"{0}\". Please specify \"resourceName:\" for this parameter in constructor.", "resourceName"));
            }

            if (string.IsNullOrWhiteSpace(_apiVersion))
            {
                _apiVersion = DefaultOpenAIApiVersion;
            }

            resourceName = domain;
            apiVersion = _apiVersion;
            deploymentId = string.Empty;
            baseRequest = string.Format("/{0}/", _apiVersion);
            baseRequestUrlFormat = string.Format("https://{0}{1}{{0}}", resourceName, baseRequest);
            useOAuthAuthentication = true;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="OpenAIClientSettings"/> for use with Azure OpenAI.<br/>
        /// <see href="https://learn.microsoft.com/en-us/azure/cognitive-services/openai/"/>
        /// </summary>
        /// <param name="resourceName">
        /// The name of your Azure OpenAI Resource.
        /// </param>
        /// <param name="deploymentId">
        /// The name of your model deployment. You're required to first deploy a model before you can make calls.
        /// </param>
        /// <param name="apiVersion">
        /// Optional, defaults to 2022-12-01
        /// </param>
        /// <param name="useActiveDirectoryAuthentication">
        /// Optional, set to true if you want to use Azure Active Directory for Authentication.
        /// </param>
        public OpenAIClientSettings(string _resourceName, string _deploymentId, string _apiVersion = DefaultAzureApiVersion, bool useActiveDirectoryAuthentication = false)
        {
            if (string.IsNullOrWhiteSpace(_resourceName))
            {
                throw new ArgumentNullException("_resourceName");
            }

            if (resourceName.Contains(".") ||
                resourceName.Contains(":"))
            {
                throw new ArgumentException(string.Format("You're attempting to pass a \"domain\" parameter to \"{0}\". Please specify \"domain:\" for this parameter in constructor.", "resourceName"));
            }

            if (string.IsNullOrWhiteSpace(apiVersion))
            {
                apiVersion = DefaultAzureApiVersion;
            }

            resourceName = _resourceName;
            deploymentId = _deploymentId;
            apiVersion = _apiVersion;
            baseRequest = string.Format("/openai/deployments/{0}/", deploymentId);
            baseRequestUrlFormat = string.Format("https://{0}.{1}{2}{{0}}", resourceName, AzureOpenAIDomain, baseRequest);
            defaultQueryParameters.Add("api-version", apiVersion);
            useOAuthAuthentication = useActiveDirectoryAuthentication;
        }

        public string resourceName { get; set; }

        public string apiVersion { get; set; }

        public string deploymentId { get; set; }

        internal string baseRequest { get; set; }

        internal string baseRequestUrlFormat { get; set; }

        internal bool useOAuthAuthentication { get; set; }

        internal bool ssAzureDeployment { get { return baseRequestUrlFormat.Contains(AzureOpenAIDomain); } }

        private readonly Dictionary<string, string> defaultQueryParameters = new Dictionary<string, string>();

        internal IReadOnlyDictionary<string, string> DefaultQueryParameters { get { return defaultQueryParameters; } }

        public static OpenAIClientSettings Default { get { return new OpenAIClientSettings(); } }
    }
}
