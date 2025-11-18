using System.Collections.Concurrent;
using Line.Messaging.API.Common;

namespace Line.Messaging.API.Helpers
{
    /// <summary>
    /// Helper class for managing API rate limits.
    /// Supports both per-channel and global rate limiting.
    /// </summary>
    public class RateLimitHelper
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<ApiEndpointType, RateLimitInfo>> _rateLimitStore =
            new ConcurrentDictionary<string, ConcurrentDictionary<ApiEndpointType, RateLimitInfo>>();

        /// <summary>
        /// Gets the rate limit configuration for a specific endpoint type
        /// </summary>
        public static RateLimitConfig GetRateLimitConfig(ApiEndpointType endpointType)
        {
            return endpointType switch
            {
                ApiEndpointType.LowFrequencyHourly => new RateLimitConfig(60, TimeSpan.FromHours(1)),
                ApiEndpointType.AudienceOperations => new RateLimitConfig(60, TimeSpan.FromMinutes(1)),
                ApiEndpointType.WebhookManagement => new RateLimitConfig(1000, TimeSpan.FromMinutes(1)),
                ApiEndpointType.RichMenuOperations => new RateLimitConfig(100, TimeSpan.FromHours(1)),
                ApiEndpointType.RichMenuBatchOperations => new RateLimitConfig(3, TimeSpan.FromHours(1)),
                ApiEndpointType.MediumFrequency => new RateLimitConfig(200, TimeSpan.FromSeconds(1)),
                ApiEndpointType.LoadingAnimation => new RateLimitConfig(100, TimeSpan.FromSeconds(1)),
                ApiEndpointType.TokenIssuance => new RateLimitConfig(370, TimeSpan.FromSeconds(1)),
                ApiEndpointType.OtherEndpoints => new RateLimitConfig(2000, TimeSpan.FromSeconds(1)),
                _ => new RateLimitConfig(2000, TimeSpan.FromSeconds(1))
            };
        }

        /// <summary>
        /// Checks if a request can be made for the given channel and endpoint type.
        /// For single channel applications, use "global" as the key.
        /// </summary>
        /// <param name="channelId">The channel ID (used as key for rate limiting). If null or empty, uses "global" as the key.</param>
        /// <param name="endpointType">The type of API endpoint</param>
        /// <returns>True if the request can be made, false if rate limit is exceeded</returns>
        public static bool CanMakeRequest(string? channelId, ApiEndpointType endpointType)
        {
            // Use "global" as key if channelId is not provided (for single channel applications)
            var key = string.IsNullOrWhiteSpace(channelId) ? "global" : channelId;

            var config = GetRateLimitConfig(endpointType);
            var channelLimits = _rateLimitStore.GetOrAdd(key, _ => new ConcurrentDictionary<ApiEndpointType, RateLimitInfo>());
            var limitInfo = channelLimits.GetOrAdd(endpointType, _ => new RateLimitInfo(config));

            lock (limitInfo)
            {
                var now = DateTime.UtcNow;

                // Reset if the time window has passed
                if (now >= limitInfo.WindowResetTime)
                {
                    limitInfo.RequestCount = 0;
                    limitInfo.WindowStartTime = now;
                    limitInfo.WindowResetTime = now.Add(config.WindowSize);
                }

                // Check if limit is exceeded
                if (limitInfo.RequestCount >= config.MaxRequests)
                {
                    return false;
                }

                // Increment request count
                limitInfo.RequestCount++;
                return true;
            }
        }

        /// <summary>
        /// Gets the current rate limit status for a channel and endpoint type
        /// </summary>
        /// <param name="channelId">The channel ID. If null or empty, uses "global" as the key.</param>
        /// <param name="endpointType">The type of API endpoint</param>
        /// <returns>Rate limit status information</returns>
        public static RateLimitStatus GetRateLimitStatus(string? channelId, ApiEndpointType endpointType)
        {
            // Use "global" as key if channelId is not provided
            var key = string.IsNullOrWhiteSpace(channelId) ? "global" : channelId;

            var config = GetRateLimitConfig(endpointType);
            var channelLimits = _rateLimitStore.GetOrAdd(key, _ => new ConcurrentDictionary<ApiEndpointType, RateLimitInfo>());
            var limitInfo = channelLimits.GetOrAdd(endpointType, _ => new RateLimitInfo(config));

            lock (limitInfo)
            {
                var now = DateTime.UtcNow;

                // Reset if the time window has passed
                if (now >= limitInfo.WindowResetTime)
                {
                    limitInfo.RequestCount = 0;
                    limitInfo.WindowStartTime = now;
                    limitInfo.WindowResetTime = now.Add(config.WindowSize);
                }

                return new RateLimitStatus
                {
                    RemainingRequests = Math.Max(0, config.MaxRequests - limitInfo.RequestCount),
                    ResetTime = limitInfo.WindowResetTime,
                    Limit = config.MaxRequests,
                    Used = limitInfo.RequestCount
                };
            }
        }

        /// <summary>
        /// Resets the rate limit for a specific channel and endpoint type
        /// </summary>
        /// <param name="channelId">The channel ID</param>
        /// <param name="endpointType">The type of API endpoint</param>
        public static void ResetRateLimit(string channelId, ApiEndpointType endpointType)
        {
            if (string.IsNullOrWhiteSpace(channelId))
                return;

            if (_rateLimitStore.TryGetValue(channelId, out var channelLimits))
            {
                if (channelLimits.TryRemove(endpointType, out _))
                {
                    // If no more endpoint types for this channel, remove the channel entry
                    if (channelLimits.IsEmpty)
                    {
                        _rateLimitStore.TryRemove(channelId, out _);
                    }
                }
            }
        }

        /// <summary>
        /// Resets all rate limits for a specific channel
        /// </summary>
        /// <param name="channelId">The channel ID</param>
        public static void ResetAllRateLimits(string channelId)
        {
            if (string.IsNullOrWhiteSpace(channelId))
                return;

            _rateLimitStore.TryRemove(channelId, out _);
        }

        /// <summary>
        /// Internal class to store rate limit information
        /// </summary>
        private class RateLimitInfo
        {
            public int RequestCount { get; set; }
            public DateTime WindowStartTime { get; set; }
            public DateTime WindowResetTime { get; set; }

            public RateLimitInfo(RateLimitConfig config)
            {
                var now = DateTime.UtcNow;
                RequestCount = 0;
                WindowStartTime = now;
                WindowResetTime = now.Add(config.WindowSize);
            }
        }
    }

    /// <summary>
    /// Configuration for rate limits
    /// </summary>
    public class RateLimitConfig
    {
        public int MaxRequests { get; }
        public TimeSpan WindowSize { get; }

        public RateLimitConfig(int maxRequests, TimeSpan windowSize)
        {
            MaxRequests = maxRequests;
            WindowSize = windowSize;
        }
    }

    /// <summary>
    /// Current rate limit status
    /// </summary>
    public class RateLimitStatus
    {
        public int RemainingRequests { get; set; }
        public DateTime ResetTime { get; set; }
        public int Limit { get; set; }
        public int Used { get; set; }
    }
}

