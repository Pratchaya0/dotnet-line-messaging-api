using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Common;
using Line.Messaging.API.Models.Message;
using Line.Messaging.API.Models.Response;
using RestSharp;

namespace Line.Messaging.API.Clients.Message
{
    /// <summary>
    /// Client for LINE Messaging API message operations
    /// </summary>
    public class MessageClient : MessagingAPIClient
    {
        /// <summary>
        /// Initializes a new instance of MessageClient with default endpoint
        /// </summary>
        /// <param name="channelAccessToken">The channel access token</param>
        public MessageClient(string channelAccessToken)
            : this(DomainName.OTHER_ENDPOINT, channelAccessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of MessageClient
        /// </summary>
        /// <param name="instanceUrl">The base URL for the API instance</param>
        /// <param name="channelAccessToken">The channel access token</param>
        public MessageClient(string instanceUrl, string channelAccessToken)
            : base(instanceUrl, channelAccessToken)
        {
        }

        public async Task<Result<HeaderResponse, SentMessagesResponse, MessageResponse>> SendReplyMessageAsync(
            string replyToken,
            List<BaseMessage> messages,
            bool? notificationDisabled = false,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(replyToken))
            {
                throw new ArgumentNullException(nameof(replyToken));
            }

            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "v2/bot/message/reply";
            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                replyToken,
                messages,
                notificationDisabled
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<SentMessagesResponse>(response.Content ?? "{}")
                    ?? new SentMessagesResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, SentMessagesResponse, MessageResponse>> SendPushMessageAsync(
            string to,
            List<BaseMessage> messages,
            bool? notificationDisabled = false,
            Guid? retryKey = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException(nameof(to));
            }

            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/push";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);
            if (retryKey.HasValue && retryKey != Guid.Empty)
                request.AddJsonBody("X-Line-Retry-Key", retryKey.ToString());

            request.AddJsonBody(new
            {
                to,
                messages,
                notificationDisabled
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<SentMessagesResponse>(response.Content ?? "{}")
                    ?? new SentMessagesResponse()
                ),
                StatusCodeType.Conflict => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Duplicate request" }
                ),
                StatusCodeType.TooManyRequests => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Too many requests" }
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, SentMessagesResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageResponse>> SendMulticastMessageAsync(
            List<string> to,
            List<BaseMessage> messages,
            bool? notificationDisabled = false,
            Guid? retryKey = null,
            CancellationToken cancellationToken = default)
        {

            if (to.Count == 0 || to.Count > 500)
            {
                throw new ArgumentOutOfRangeException(nameof(to), "The number of recipients must be between 1 and 500.");
            }

            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/multicast";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);
            if (retryKey.HasValue && retryKey != Guid.Empty)
                request.AddJsonBody("X-Line-Retry-Key", retryKey.ToString());

            request.AddJsonBody(new
            {
                to,
                messages,
                notificationDisabled
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, string, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    response.Content ?? "{}"),
                StatusCodeType.Conflict => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Duplicate request" }
                ),
                StatusCodeType.TooManyRequests => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Too many requests" }
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageResponse>> SendBroadcastMessageAsync(
            List<BaseMessage> messages,
            bool? notificationDisabled = false,
            Guid? retryKey = null,
            CancellationToken cancellationToken = default)
        {

            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/broadcast";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);
            if (retryKey.HasValue && retryKey != Guid.Empty)
                request.AddJsonBody("X-Line-Retry-Key", retryKey.ToString());

            request.AddJsonBody(new
            {
                messages,
                notificationDisabled
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, string, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    response.Content ?? "{}"),
                StatusCodeType.Conflict => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Duplicate request" }
                ),
                StatusCodeType.TooManyRequests => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Too many requests" }
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageResponse>> MarkMessagesAsReadAsync(
            string markAsReadToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(markAsReadToken.Trim()))
            {
                throw new ArgumentNullException(nameof(markAsReadToken));
            }

            const string path = "/v2/bot/chat/markAsRead";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                markAsReadToken
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, string, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageDetailResponse>> DisplayALoadingAnimationAsync(
            string chatId,
            int? loadingSeconds = 20,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(chatId.Trim()))
            {
                throw new ArgumentNullException(nameof(chatId));
            }

            if (loadingSeconds % 5 > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(loadingSeconds), "Number of seconds to display a loading animation. You can specify a any one of 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, or 60.");
            }

            const string path = "/v2/bot/chat/loading/start";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                chatId,
                loadingSeconds
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
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse
                    {
                        Message = "Unknown error",
                        Details = new()
                        {
                            new MessageDetail
                            {
                                Message = "Unknown error occurred while displaying loading animation.",
                                Property = nameof(DisplayALoadingAnimationAsync)
                            }
                        }
                    }
                )
            };
        }

        public async Task<Result<HeaderResponse, LimitSendingMessageResponse, MessageResponse>>
            GetTheTargetLimitForSendingMessagesThisMonthAsync(CancellationToken cancellationToken = default)
        {
            const string path = "/v2/bot/message/quota";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, LimitSendingMessageResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<LimitSendingMessageResponse>(response.Content ?? "{}")
                    ?? new LimitSendingMessageResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, LimitSendingMessageResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, LimitSendingMessageResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, TotalUsageResponse, MessageResponse>>
            GetNumberOfMessagesSentThisMonthAsync(CancellationToken cancellationToken = default)
        {
            const string path = "/v2/bot/message/quota/consumption";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, TotalUsageResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<TotalUsageResponse>(response.Content ?? "{}")
                    ?? new TotalUsageResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, TotalUsageResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, TotalUsageResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>> GetNumberOfSentReplyMessagesAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            const string path = "/v2/bot/message/delivery/reply";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("date", date.ToString("yyyyMMdd"));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<StatusSentMessageResponse>(response.Content ?? "{}")
                    ?? new StatusSentMessageResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>> GetNumberOfSentPushMessagesAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            const string path = "/v2/bot/message/delivery/push";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("date", date.ToString("yyyyMMdd"));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<StatusSentMessageResponse>(response.Content ?? "{}")
                    ?? new StatusSentMessageResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>> GetNumberOfSentMulticastMessagesAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            const string path = "/v2/bot/message/delivery/multicast";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("date", date.ToString("yyyyMMdd"));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<StatusSentMessageResponse>(response.Content ?? "{}")
                    ?? new StatusSentMessageResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }

        public async Task<Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>> GetNumberOfSentBroadcastMessagesAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            const string path = "/v2/bot/message/delivery/broadcast";

            var request = new RestRequest(path, Method.Get);

            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddQueryParameter("date", date.ToString("yyyyMMdd"));

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<StatusSentMessageResponse>(response.Content ?? "{}")
                    ?? new StatusSentMessageResponse()
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, StatusSentMessageResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }


        public async Task<Result<HeaderResponse, string, MessageDetailResponse>> ValidateMessageObjectsOfAReplyMessageAsync(
            List<BaseMessage> messages,
            CancellationToken cancellationToken = default)
        {

            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/validate/reply";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                messages
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
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse
                    {
                        Message = "Unknown error",
                        Details = new()
                        {
                            new MessageDetail
                            {
                                Message = "Unknown error occurred while validating message objects of a reply message.",
                                Property = nameof(ValidateMessageObjectsOfAReplyMessageAsync)
                            }
                        }
                    }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageDetailResponse>> ValidateMessageObjectsOfAPushMessageAsync(
            List<BaseMessage> messages,
            CancellationToken cancellationToken = default)
        {
            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/validate/push";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                messages
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
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse
                    {
                        Message = "Unknown error",
                        Details = new()
                        {
                            new MessageDetail
                            {
                                Message = "Unknown error occurred while validating message objects of a reply message.",
                                Property = nameof(ValidateMessageObjectsOfAReplyMessageAsync)
                            }
                        }
                    }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageDetailResponse>> ValidateMessageObjectsOfAMulticastMessageAsync(
            List<BaseMessage> messages,
            CancellationToken cancellationToken = default)
        {
            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/validate/multicast";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                messages
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
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse
                    {
                        Message = "Unknown error",
                        Details = new()
                        {
                            new MessageDetail
                            {
                                Message = "Unknown error occurred while validating message objects of a reply message.",
                                Property = nameof(ValidateMessageObjectsOfAReplyMessageAsync)
                            }
                        }
                    }
                )
            };
        }

        public async Task<Result<HeaderResponse, string, MessageDetailResponse>> ValidateMessageObjectsOfABroadcastMessageAsync(
            List<BaseMessage> messages,
            CancellationToken cancellationToken = default)
        {
            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/validate/broadcast";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);

            request.AddJsonBody(new
            {
                messages
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
                    response.Content ?? "{}"),
                StatusCodeType.BadRequest => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    JsonSerializer.Deserialize<MessageDetailResponse>(response.Content ?? "{}")
                    ?? new MessageDetailResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, string, MessageDetailResponse>.Failure(
                    new MessageDetailResponse
                    {
                        Message = "Unknown error",
                        Details = new()
                        {
                            new MessageDetail
                            {
                                Message = "Unknown error occurred while validating message objects of a reply message.",
                                Property = nameof(ValidateMessageObjectsOfAReplyMessageAsync)
                            }
                        }
                    }
                )
            };
        }

        public async Task<Result<HeaderResponse, MessageSentResponse, MessageResponse>> RetryingAnApiRequestAsync(
            Guid retryKey,
            List<BaseMessage> messages,
            CancellationToken cancellationToken = default)
        {
            if (messages.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "The maximum number of messages is 5.");
            }

            if (messages.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(messages), "At least one message is required.");
            }

            const string path = "/v2/bot/message/push";

            var request = new RestRequest(path, Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", AuthorizationHeaderValue);
            request.AddJsonBody("X-Line-Retry-Key", retryKey.ToString());

            request.AddJsonBody(new
            {
                messages,
            });

            var response = await Client.ExecuteAsync(request, cancellationToken);

            return (StatusCodeType)response.StatusCode switch
            {
                StatusCodeType.OK => Result<HeaderResponse, MessageSentResponse, MessageResponse>.Success(
                    new HeaderResponse
                    {
                        XLineRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Request-Id")?.Value?.ToString() ?? string.Empty,
                        XLineAcceptedRequestId = response.Headers?.FirstOrDefault(h => h.Name == "X-Line-Accepted-Request-Id")?.Value?.ToString() ?? string.Empty
                    },
                    JsonSerializer.Deserialize<MessageSentResponse>(response.Content ?? "{}")
                    ?? new MessageSentResponse()
                ),
                StatusCodeType.Conflict => Result<HeaderResponse, MessageSentResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Duplicate request" }
                ),
                StatusCodeType.TooManyRequests => Result<HeaderResponse, MessageSentResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}") ??
                    new MessageResponse { Message = "Too many requests" }
                ),
                StatusCodeType.BadRequest => Result<HeaderResponse, MessageSentResponse, MessageResponse>.Failure(
                    JsonSerializer.Deserialize<MessageResponse>(response.Content ?? "{}")
                    ?? new MessageResponse { Message = "Bad request" }
                ),
                _ => Result<HeaderResponse, MessageSentResponse, MessageResponse>.Failure(
                    new MessageResponse { Message = "Unknown error" }
                )
            };
        }
    }
}
