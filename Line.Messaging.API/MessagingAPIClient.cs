using Line.Messaging.API.Clients.Message;
using Line.Messaging.API.Common;
using RestSharp;

namespace Line.Messaging.API
{
    /// <summary>
    /// Base class for LINE Messaging API clients
    /// </summary>
    public abstract class MessagingAPIClient : IDisposable
    {
        /// <summary>
        /// RestClient instance for making HTTP requests
        /// </summary>
        public RestClient Client { get; }

        /// <summary>
        /// Authorization header value (Bearer token)
        /// </summary>
        public string AuthorizationHeaderValue { get; }

        /// <summary>
        /// Initializes a new instance of MessagingAPIClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        protected MessagingAPIClient(string instanceUrl, string channelAccessToken)
        {
            if (string.IsNullOrWhiteSpace(instanceUrl))
                throw new ArgumentException("Instance URL cannot be null or empty", nameof(instanceUrl));

            if (string.IsNullOrWhiteSpace(channelAccessToken))
                throw new ArgumentException("Channel access token cannot be null or empty", nameof(channelAccessToken));

            var options = new RestClientOptions
            {
                BaseUrl = new Uri(instanceUrl),
                ThrowOnAnyError = true,
            };

            Client = new RestClient(options);
            AuthorizationHeaderValue = $"Bearer {channelAccessToken}";
        }

        /// <summary>
        /// Disposes the RestClient
        /// </summary>
        public void Dispose()
        {
            Client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
