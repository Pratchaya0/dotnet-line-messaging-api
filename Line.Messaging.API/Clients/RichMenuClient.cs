using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Line.Messaging.API.Common;
using Line.Messaging.API.Models.Response;
using Line.Messaging.API.Models.RichMenu;
using RestSharp;

namespace Line.Messaging.API.Clients.Message
{
    /// <summary>
    /// Client for LINE Messaging API rich menu operations
    /// </summary>
    public class RichMenuClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of RichMenuClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public RichMenuClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of RichMenuClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public RichMenuClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        public async Task<Result<RichMenuIdResponse, MessageDetailResponse>> CreateRichMenuAsync(
            BaseRichMenu richMenu,
            CancellationToken cancellationToken = default)
        {
            const string path = "v2/bot/richmenu";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddBody(richMenu);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<RichMenuIdResponse, MessageDetailResponse>.Success(
                    JsonSerializer.Deserialize<RichMenuIdResponse>(response.Content ?? "{}")
                    ?? new RichMenuIdResponse()
                ),
                StatusCodeType.BadRequest => Result<RichMenuIdResponse, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<RichMenuIdResponse, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<string, MessageDetailResponse>> ValidateRichMenuObjectAsync(
            BaseRichMenu richMenu,
            CancellationToken cancellationToken = default)
        {
            const string path = "v2/bot/richmenu/validate";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddBody(richMenu);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<string, MessageDetailResponse>.Success(response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<string, MessageResponse>> DeleteRichMenuAsync(
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            const string path = "v2/bot/richmenu/{richMenuId}";

            var request = new RestRequest(path, Method.Delete);

            request.AddUrlSegment("richMenuId", richMenuId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<string, MessageResponse>.Success(response.Content ?? "{}"),
                StatusCodeType.NotFound => Result<string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<string, MessageDetailResponse>> SetDefaultRichMenuAsync(
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            const string path = "v2/bot/user/all/richmenu/{richMenuId}";

            var request = new RestRequest(path, Method.Post);

            request.AddUrlSegment("richMenuId", richMenuId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<string, MessageDetailResponse>.Success(response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                StatusCodeType.NotFound => Result<string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Not found" }
                ),
                _ => Result<string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<RichMenuIdResponse, MessageDetailResponse>> GetDefaultRichMenuId(
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            const string path = "v2/bot/user/all/richmenu";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<RichMenuIdResponse, MessageDetailResponse>.Success(
                    JsonSerializer.Deserialize<RichMenuIdResponse>(response.Content ?? "{}")
                    ?? new RichMenuIdResponse()
                ),
                StatusCodeType.Forbidden => Result<RichMenuIdResponse, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                StatusCodeType.NotFound => Result<RichMenuIdResponse, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Not found" }
                ),
                _ => Result<RichMenuIdResponse, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<string, MessageDetailResponse>> ClearDefaultRichMenuAsync(
            CancellationToken cancellationToken = default)
        {
            const string path = "v2/bot/user/all/richmenu";

            var request = new RestRequest(path, Method.Delete);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<string, MessageDetailResponse>.Success(response.Content ?? "{}"),
                _ => Result<string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }
    }
}
