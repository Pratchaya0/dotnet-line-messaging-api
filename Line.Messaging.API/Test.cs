using System;
using System.Threading.Tasks;
using Line.Messaging.API.Clients.Message;

namespace Line.Messaging.API
{
    /// <summary>
    /// Example usage of MessageClient
    /// </summary>
    public class Test
    {
        private readonly MessageClient _client;

        /// <summary>
        /// Initializes a new instance of Test with MessageClient
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public Test(string channelAccessToken)
        {
            if (string.IsNullOrWhiteSpace(channelAccessToken))
                throw new ArgumentException("Channel access token cannot be null or empty", nameof(channelAccessToken));

            // Create MessageClient with default endpoint
            _client = new MessageClient(channelAccessToken);
        }

        /// <summary>
        /// Example: Get number of sent reply messages
        /// </summary>
        public async Task GetNumberOfSentReplyMessagesExample(DateTime date)
        {
            var result = await _client.GetNumberOfSentReplyMessagesAsync(date);

            throw new NotImplementedException();
        }
    }
}
