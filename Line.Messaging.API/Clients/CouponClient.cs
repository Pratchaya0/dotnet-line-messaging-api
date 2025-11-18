using System;
using Line.Messaging.API.Common;

namespace Line.Messaging.API.Clients.Message
{
    /// <summary>
    /// Client for LINE Messaging API coupon operations
    /// </summary>
    public class CouponClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of CouponClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public CouponClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of CouponClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public CouponClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        // TODO: Implement coupon methods
    }
}
