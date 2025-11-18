using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Line.Messaging.API.Common;
using Line.Messaging.API.Models.Response;
using RestSharp;

namespace Line.Messaging.API.Clients.Message
{
    /// <summary>
    /// Client for LINE Messaging API insight operations
    /// </summary>
    public class InsightClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of InsightClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public InsightClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of InsightClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public InsightClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        public async Task<Result<DeliveryNumberResponse, MessageResponse>> GetNumberOfMessageDeliveriesAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {

            const string path = "v2/bot/insight/message/delivery";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("date", date.ToString("yyyyMMdd"));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<DeliveryNumberResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<DeliveryNumberResponse>(response.Content ?? "{}")
                    ?? new DeliveryNumberResponse()
                ),
                StatusCodeType.BadRequest => Result<DeliveryNumberResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<DeliveryNumberResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<FollowerNumberResponse, MessageResponse>> GetNumberOfFollowersAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            const string path = "v2/bot/insight/followers";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter($"date", date.ToString("yyyyMMdd"));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<FollowerNumberResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<FollowerNumberResponse>(response.Content ?? "{}")
                    ?? new FollowerNumberResponse()
                ),
                StatusCodeType.BadRequest => Result<FollowerNumberResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<FollowerNumberResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<FriendDemographicsResponse, MessageResponse>> GetFriendDemographicsAsync(
            CancellationToken cancellationToken = default)
        {
            const string path = "v2/bot/insight/demographic";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<FriendDemographicsResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<FriendDemographicsResponse>(response.Content ?? "{}")
                    ?? new FriendDemographicsResponse()
                ),
                _ => Result<FriendDemographicsResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<InteractionStatisticResponse, MessageResponse>> GetUserInteractionStatisticsAsync(
            Guid requestId,
            CancellationToken cancellationToken = default)
        {
            if (requestId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(requestId));
            }

            const string path = "v2/bot/insight/message/event";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("requestId", requestId.ToString());

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<InteractionStatisticResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<InteractionStatisticResponse>(response.Content ?? "{}")
                    ?? new InteractionStatisticResponse()
                ),
                _ => Result<InteractionStatisticResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }
    }
}
