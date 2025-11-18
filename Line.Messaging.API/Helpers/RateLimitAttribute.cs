using System.Net;
using Line.Messaging.API.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Line.Messaging.API.Helpers
{
    /// <summary>
    /// Action filter attribute to enforce rate limiting on API endpoints.
    /// Uses global rate limit (single channel application).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        private readonly ApiEndpointType _endpointType;
        private const string GLOBAL_KEY = "global";

        /// <summary>
        /// Initializes a new instance of the RateLimitAttribute
        /// </summary>
        /// <param name="endpointType">The type of API endpoint being rate limited</param>
        public RateLimitAttribute(ApiEndpointType endpointType)
        {
            _endpointType = endpointType;
        }

        /// <summary>
        /// Called before the action executes
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Use global rate limit (single channel application)
            if (!RateLimitHelper.CanMakeRequest(GLOBAL_KEY, _endpointType))
            {
                var status = RateLimitHelper.GetRateLimitStatus(GLOBAL_KEY, _endpointType);

                context.Result = new ObjectResult(new
                {
                    message = "Rate limit exceeded. Too many requests.",
                    error = "TooManyRequests",
                    rateLimit = new
                    {
                        limit = status.Limit,
                        remaining = status.RemainingRequests,
                        reset = status.ResetTime
                    }
                })
                {
                    StatusCode = (int)HttpStatusCode.TooManyRequests
                };

                // Add rate limit headers
                context.HttpContext.Response.Headers["X-RateLimit-Limit"] = status.Limit.ToString();
                context.HttpContext.Response.Headers["X-RateLimit-Remaining"] = status.RemainingRequests.ToString();
                context.HttpContext.Response.Headers["X-RateLimit-Reset"] = status.ResetTime.ToUnixTimeSeconds().ToString();
                return;
            }

            // Add rate limit headers for successful requests
            var currentStatus = RateLimitHelper.GetRateLimitStatus(GLOBAL_KEY, _endpointType);
            context.HttpContext.Response.Headers["X-RateLimit-Limit"] = currentStatus.Limit.ToString();
            context.HttpContext.Response.Headers["X-RateLimit-Remaining"] = currentStatus.RemainingRequests.ToString();
            context.HttpContext.Response.Headers["X-RateLimit-Reset"] = currentStatus.ResetTime.ToUnixTimeSeconds().ToString();

            base.OnActionExecuting(context);
        }
    }

    /// <summary>
    /// Extension methods for DateTime to convert to Unix timestamp
    /// </summary>
    internal static class DateTimeExtensions
    {
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(dateTime.ToUniversalTime() - epoch).TotalSeconds;
        }
    }
}

