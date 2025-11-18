using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Common;
using Line.Messaging.API.Models.Response;
using RestSharp;

namespace Line.Messaging.API.Clients
{
    public class RichMenuAliasClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of InsightClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public RichMenuAliasClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of InsightClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public RichMenuAliasClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        public async Task<Result<string, MessageDetailResponse>> CreateRichMenuAliasAsync(
            string richMenuAliasId,
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            if (string.IsNullOrEmpty(richMenuAliasId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuAliasId));
            }

            const string path = "v2/bot/richmenu/alias";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                richMenuAliasId,
                richMenuId,
            });

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

        public async Task<Result<string, MessageDetailResponse>> DeleteRichMenuAliasAsync(
            string richMenuAliasId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuAliasId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuAliasId));
            }

            const string path = "v2/bot/richmenu/alias/{richMenuAliasId}";

            var request = new RestRequest(path, Method.Delete);

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

        public async Task<Result<string, MessageDetailResponse>> UpdateRichMenuAliasAsync(
            string richMenuAliasId,
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            if (string.IsNullOrEmpty(richMenuAliasId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuAliasId));
            }

            const string path = "v2/bot/richmenu/alias/{richMenuAliasId}";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Authorization", AuthorizationHeaderValue);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new { richMenuId });

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

        public async Task<Result<AliasResponse, MessageDetailResponse>> GetRichMenuAliasInformationAsync(
            string richMenuAliasId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuAliasId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuAliasId));
            }

            const string path = "v2/bot/richmenu/alias/{richMenuAliasId}";

            var request = new RestRequest(path, Method.Get);

            request.AddUrlSegment("richMenuAliasId", richMenuAliasId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<AliasResponse, MessageDetailResponse>.Success(
                    JsonSerializer.Deserialize<AliasResponse>(response.Content ?? "{}")
                    ?? new AliasResponse()
                ),
                StatusCodeType.BadRequest => Result<AliasResponse, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                StatusCodeType.NotFound => Result<AliasResponse, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Not found" }
                ),
                _ => Result<AliasResponse, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<AliasListResponse, MessageDetailResponse>> GetListOfRichMenuAliasAsync(
            CancellationToken cancellationToken = default)
        {
            const string path = "v2/bot/richmenu/alias/list";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<AliasListResponse, MessageDetailResponse>.Success(
                    JsonSerializer.Deserialize<AliasListResponse>(response.Content ?? "{}")
                    ?? new AliasListResponse()
                ),
                _ => Result<AliasListResponse, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }
    }
}
