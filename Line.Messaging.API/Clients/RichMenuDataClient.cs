using System.IO;
using System.Text.Json;
using Line.Messaging.API.Common;
using Line.Messaging.API.Models.Response;
using RestSharp;

namespace Line.Messaging.API.Clients
{
    /// <summary>
    /// Client for LINE Messaging API rich menu data operations (upload/download images)
    /// </summary>
    public class RichMenuDataClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of RichMenuDataClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public RichMenuDataClient(string channelAccessToken)
            : this(DomainName.DATA_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of RichMenuDataClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public RichMenuDataClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        /// <summary>
        /// Uploads a rich menu image
        /// </summary>
        /// <param name="richMenuId">The rich menu ID</param>
        /// <param name="imageStream">The image file stream</param>
        /// <param name="contentType">The content type (e.g., "image/jpeg", "image/png")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result with HeaderResponse and success/error response</returns>
        public async Task<Result<string, MessageResponse>> UploadRichMenuImageAsync(
            string richMenuId,
            Stream imageStream,
            string contentType,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(richMenuId))
            {
                throw new ArgumentException("Rich menu ID cannot be null or empty", nameof(richMenuId));
            }

            if (imageStream == null)
            {
                throw new ArgumentNullException(nameof(imageStream));
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentException("Content type cannot be null or empty", nameof(contentType));
            }

            var allowedContentTypes = new List<string> { "image/jpeg", "image/png" };
            if (!allowedContentTypes.Contains(contentType))
            {
                throw new ArgumentException(
                    $"Content type must be one of: {string.Join(", ", allowedContentTypes)}. Provided: {contentType}",
                    nameof(contentType));
            }

            const string path = "/v2/bot/richmenu/{richMenuId}/content";

            var request = new RestRequest(path, Method.Post);

            request.AddUrlSegment("richMenuId", richMenuId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);
            request.AddHeader("Content-Type", contentType);

            byte[] payload;
            if (imageStream is MemoryStream memoryStream)
            {
                payload = memoryStream.ToArray();
            }
            else
            {
                using var copy = new MemoryStream();
                await imageStream.CopyToAsync(copy, cancellationToken);
                payload = copy.ToArray();
            }

            request.AddParameter(new BodyParameter(string.Empty, payload, contentType));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<string, MessageResponse>.Success(
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        /// <summary>
        /// Downloads a rich menu image
        /// </summary>
        /// <param name="richMenuId">The rich menu ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result with image binary data or error response</returns>
        public async Task<Result<byte[], MessageResponse>> DownloadRichMenuImageAsync(
            string richMenuId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(richMenuId))
            {
                throw new ArgumentException("Rich menu ID cannot be null or empty", nameof(richMenuId));
            }

            const string path = "/v2/bot/richmenu/{richMenuId}/content";

            var request = new RestRequest(path, Method.Get);

            request.AddUrlSegment("richMenuId", richMenuId);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<byte[], MessageResponse>.Success(
                    response.RawBytes ?? Array.Empty<byte>()),
                StatusCodeType.NotFound => Result<byte[], MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Not found" }
                ),
                StatusCodeType.BadRequest => Result<byte[], MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<byte[], MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }
    }
}
