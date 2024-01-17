using System.Collections.Generic;
using System.Linq;

namespace OpenAI
{
    public abstract class BaseEndPoint
    {
        protected BaseEndPoint(OpenAIClient client) { this.client = client; }

        // ReSharper disable once InconsistentNaming
        protected readonly OpenAIClient client;

        /// <summary>
        /// The root endpoint address.
        /// </summary>
        protected abstract string Root { get; }

        /// <summary>
        /// Gets the full formatted url for the API endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint url.</param>
        /// <param name="queryParameters">Optional, parameters to add to the endpoint.</param>
        protected string GetUrl(string endpoint = "", Dictionary<string, string> queryParameters = null)
        {
            var result = string.Format(client.openAIClientSettings.baseRequestUrlFormat, string.Format("{0}{1}", Root, endpoint));

            foreach (var defaultQueryParameter in client.openAIClientSettings.DefaultQueryParameters)
            {
                queryParameters = queryParameters ?? new Dictionary<string, string>();
                queryParameters.Add(defaultQueryParameter.Key, defaultQueryParameter.Value);
            }

            if (queryParameters.Count > 0)
            {
                result += string.Format("?{0}", string.Join("&", queryParameters.Select(parameter => string.Format("{0}={1}", parameter.Key, parameter.Value))));
            }

            return result;
        }

        private bool enableDebug;

        /// <summary>
        /// Enables or disables the logging of all http responses of header and body information for this endpoint.<br/>
        /// WARNING! Enabling this in your production build, could potentially leak sensitive information!
        /// </summary>
        public bool EnableDebug
        {
            get { return enableDebug || client.EnableDebug;}
            set { enableDebug = value; }
        }
    }
}
