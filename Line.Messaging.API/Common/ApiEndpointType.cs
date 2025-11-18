namespace Line.Messaging.API.Common
{
    /// <summary>
    /// Represents different LINE Messaging API endpoints with their rate limits
    /// </summary>
    public enum ApiEndpointType
    {
        /// <summary>
        /// Send a narrowcast message, Send a broadcast message, Get number of message deliveries,
        /// Get number of friends, Get friend demographics, Get user interaction statistics,
        /// Get statistics per unit, Test webhook endpoint
        /// Rate limit: 60 requests per hour
        /// </summary>
        LowFrequencyHourly = 1,

        /// <summary>
        /// Create audience for uploading user IDs (by JSON), Create audience for uploading user IDs (by file),
        /// Add user IDs or Identifiers for Advertisers (IFAs) to an audience for uploading user IDs (by JSON),
        /// Add user IDs or Identifiers for Advertisers (IFAs) to an audience for uploading user IDs (by file),
        /// Create message click audience, Create message impression audience, Rename an audience,
        /// Delete audience, Get audience data, Get data for multiple audiences,
        /// Get shared audience data in Business Manager, Get a list of shared audiences in Business Manager
        /// Rate limit: 60 requests per minute
        /// </summary>
        AudienceOperations = 2,

        /// <summary>
        /// Set webhook endpoint URL, Get webhook endpoint information
        /// Rate limit: 1,000 requests per minute
        /// </summary>
        WebhookManagement = 3,

        /// <summary>
        /// Create rich menu, Delete rich menu, Delete rich menu alias,
        /// Get the status of rich menu batch control
        /// Rate limit: 100 requests per hour
        /// </summary>
        RichMenuOperations = 4,

        /// <summary>
        /// Replace or unlink the linked rich menus in batches
        /// Rate limit: 3 requests per hour
        /// </summary>
        RichMenuBatchOperations = 5,

        /// <summary>
        /// Send multicast message, Get a user's membership subscription status,
        /// Get membership plans being offered, Create a coupon, Discontinue a coupon,
        /// Get a list of coupons, Get details of a coupon
        /// Rate limit: 200 requests per second
        /// </summary>
        MediumFrequency = 6,

        /// <summary>
        /// Display a loading animation
        /// Rate limit: 100 requests per second
        /// </summary>
        LoadingAnimation = 7,

        /// <summary>
        /// Issue short-lived channel access token
        /// Rate limit: 370 requests per second
        /// </summary>
        TokenIssuance = 8,

        /// <summary>
        /// Other API endpoints
        /// Rate limit: 2,000 requests per second
        /// </summary>
        OtherEndpoints = 9
    }
}

