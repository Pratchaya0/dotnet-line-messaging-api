using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Common;
using Line.Messaging.API.Helpers;
using Line.Messaging.API.Models.Response;
using Line.Messaging.API.Models.RichMenu;
using RestSharp;

namespace Line.Messaging.API.Clients
{
    public class PerUserRichMenuClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of InsightClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public PerUserRichMenuClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of InsightClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public PerUserRichMenuClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        public async Task<Result<string, MessageResponse>> LinkRichMenuToUserAsync(
            string userId,
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId.Trim()))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            const string path = "v2/bot/user/{userId}/richmenu/{richMenuId}";

            var request = new RestRequest(path, Method.Post);

            request.AddUrlSegment("userId", userId);
            request.AddUrlSegment("richMenuId", richMenuId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<string, MessageResponse>.Success(response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                StatusCodeType.NotFound => Result<string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Not found" }
                ),
                _ => Result<string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        /// <summary>
        /// Links a rich menu to multiple users
        /// </summary>
        /// <param name="richMenuId">The rich menu ID</param>
        /// <param name="userIds">List of user IDs (must be between 1 and 500)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result with success/error response</returns>
        public async Task<Result<string, MessageDetailResponse>> LinkRichMenuToMultipleUsersAsync(
            string richMenuId,
            List<string> userIds,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(richMenuId.Trim()))
            {
                throw new ArgumentNullException(nameof(richMenuId));
            }

            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }

            if (userIds.Count < 1 || userIds.Count > 500)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(userIds),
                    userIds.Count,
                    "User IDs count must be between 1 and 500. The number of user IDs must be at least 1 and at most 500.");
            }

            const string path = "v2/bot/richmenu/bulk/link";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var requestBody = new
            {
                richMenuId,
                userIds
            };

            request.AddBody(requestBody);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.Accepted => Result<string, MessageDetailResponse>.Success(response.Content ?? "{}"),
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

        public async Task<Result<RichMenuIdResponse, MessageDetailResponse>> GetRichMenuIdOfUser(
            string userId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId.Trim()))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            const string path = "v2/bot/user/{userId}/richmenu";

            var request = new RestRequest(path, Method.Get);

            request.AddUrlSegment("userId", userId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.Accepted => Result<RichMenuIdResponse, MessageDetailResponse>.Success(
                    JsonSerializer.Deserialize<RichMenuIdResponse>(response.Content ?? "{}")
                    ?? new RichMenuIdResponse()
                ),
                StatusCodeType.BadRequest => Result<RichMenuIdResponse, MessageDetailResponse>.Failure(
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

        public async Task<Result<string, MessageResponse>> UnlinkRichMenuFromUserAsync(
            string userId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId.Trim()))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            const string path = "v2/bot/user/{userId}/richmenu";

            var request = new RestRequest(path, Method.Delete);

            request.AddUrlSegment("userId", userId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.Accepted => Result<string, MessageResponse>.Success(response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<string, MessageResponse>> UnlinkRichMenusFromMultipleUsersAsync(
            List<string> userIds,
            CancellationToken cancellationToken = default)
        {
            if (userIds.Count < 1 || userIds.Count > 500)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(userIds),
                    userIds.Count,
                    "User IDs count must be between 1 and 500. The number of user IDs must be at least 1 and at most 500.");
            }

            const string path = "v2/bot/richmenu/bulk/unlink";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new { userIds });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.Accepted => Result<string, MessageResponse>.Success(response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageDetailResponse>> ReplaceOrUnlinkTheLinkedRichMenusInBatches(
            List<RichMenuOperation> operations,
            string? resumeRequestKey = null,
            CancellationToken cancellationToken = default)
        {
            if (operations.Count < 1 || operations.Count > 1000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(operations),
                    operations.Count,
                    "Operations count must be between 1 and 1000. The number of operations must be at least 1 and at most 1000.");
            }

            if (resumeRequestKey == null)
            {
                resumeRequestKey = ResumeRequestKeyHelper.Generate(50);
            }

            const string path = "v2/bot/richmenu/batch";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Authorization", AuthorizationHeaderValue);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                operations,
                resumeRequestKey
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, string, MessageDetailResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    response.Content ?? "{}"
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                StatusCodeType.NotFound => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Not found" }
                ),
                _ => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<PhaseResponse, MessageResponse>> GetTheStatusOfRichMenuBatchControlAsync(
            string requestId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(requestId.Trim()))
            {
                throw new ArgumentNullException(nameof(requestId));
            }

            const string path = "v2/bot/richmenu/progress/batch";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("requestId", requestId);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<PhaseResponse, MessageResponse>.Success(
                    JsonSerializer.Deserialize<PhaseResponse>(response.Content ?? "{}")
                    ?? new PhaseResponse()
                ),
                StatusCodeType.BadRequest => Result<PhaseResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                StatusCodeType.NotFound => Result<PhaseResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<PhaseResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<string, MessageDetailResponse>> ValidateARequestOfRichMenuBatchControlAsync(
            List<RichMenuOperation> operations,
            string? resumeRequestKey = null,
            CancellationToken cancellationToken = default)
        {
            if (operations.Count < 1 || operations.Count > 1000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(operations),
                    operations.Count,
                    "Operations count must be between 1 and 1000. The number of operations must be at least 1 and at most 1000.");
            }

            if (resumeRequestKey == null)
            {
                resumeRequestKey = ResumeRequestKeyHelper.Generate(50);
            }

            const string path = "v2/bot/richmenu/validate/batch";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Authorization", AuthorizationHeaderValue);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                operations,
                resumeRequestKey
            });

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
    }
}
