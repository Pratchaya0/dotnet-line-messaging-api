using System;
using System.Text.Json;
using Line.Messaging.API.Common;
using Line.Messaging.API.Models.Response;
using RestSharp;

namespace Line.Messaging.API.Clients.Message
{
    /// <summary>
    /// Client for LINE Messaging API user operations
    /// </summary>
    public class UserClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of UserClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public UserClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of UserClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public UserClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        public async Task<Result<ProfileResponse, MessageResponse>> GetProfileAsync(
            string userId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId.Trim()))
            {
                throw new ArgumentException($"UserId '{userId}' is invalid.");
            }

            var path = $"v2/bot/profile/{userId}";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<ProfileResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<ProfileResponse>(response.Content ?? "{}")
                    ?? new ProfileResponse()
                ),
                StatusCodeType.BadRequest => Result<ProfileResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<ProfileResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<AddedUserResponse, MessageResponse>> GetAListOfUsersWhoAddedYourLineOfficialAccountAsAFriendAsync(
            int? limit = 300,
            string? start = null,
            CancellationToken cancellationToken = default)
        {
            if (limit > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), limit, "Limit cannot exceed 1000 for this API.");
            }

            const string path = "v2/bot/followers/ids";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddParameter("limit", limit.ToString());

            if (start != null)
                request.AddParameter("start", start);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<AddedUserResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<AddedUserResponse>(response.Content ?? "{}")
                    ?? new AddedUserResponse()
                ),
                StatusCodeType.BadRequest => Result<AddedUserResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                StatusCodeType.Unauthorized => Result<AddedUserResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "This endpoint. Only available for verified accounts or premium accounts" }
                ),
                _ => Result<AddedUserResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }
    }
}
