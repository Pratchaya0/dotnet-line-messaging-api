using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message;
using Line.Messaging.API.Models.Message.Text;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients.Message;

public class MessageClientTests
{
    [Fact]
    public async Task SendReplyMessageAsync_WhenRequestSucceeds_ReturnsHeadersAndPayload()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new
                {
                    sentMessages = new[]
                    {
                        new { id = "msg-1", quoteToken = "qt-1" }
                    }
                },
                new Dictionary<string, string>
                {
                    ["X-Line-Request-Id"] = "req-123",
                    ["X-Line-Accepted-Request-Id"] = "acc-456"
                }));

        var client = ClientTestFactory.CreateMessageClient(handler);

        var messages = new List<BaseMessage> { new TextMessage("hello world") };

        var result = await client.SendReplyMessageAsync("reply-token", messages, notificationDisabled: true);

        Assert.True(result.IsSuccess);
        Assert.Equal("req-123", result.HeaderValue?.XLineRequestId);
        Assert.Equal("acc-456", result.HeaderValue?.XLineAcceptedRequestId);
        Assert.Equal("msg-1", result.SuccessValue?.SentMessages.Single().Id);

        var request = handler.SingleRequest;
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v2/bot/message/reply", request.RequestUri?.AbsolutePath);
        Assert.Equal("Bearer test-channel-token", request.Headers["Authorization"]);

        Assert.False(string.IsNullOrWhiteSpace(request.Body), "Captured request body was empty.");

        using var payload = JsonDocument.Parse(request.Body!);
        Assert.True(payload.RootElement.TryGetProperty("replyToken", out var replyToken), request.Body);
        Assert.Equal("reply-token", replyToken.GetString());
        Assert.True(payload.RootElement.TryGetProperty("notificationDisabled", out var notificationDisabled), request.Body);
        Assert.True(notificationDisabled.GetBoolean());

        Assert.True(payload.RootElement.TryGetProperty("messages", out var messagesElement), request.Body);
        Assert.True(messagesElement.GetArrayLength() > 0, request.Body);
        var firstMessage = messagesElement[0];
        Assert.True(firstMessage.TryGetProperty("type", out var type), request.Body);
        Assert.Equal((int)MessageType.Text, type.GetInt32());
    }

    [Fact]
    public async Task GetNumberOfSentPushMessagesAsync_WhenRequestSucceeds_FormatsDateParameter()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new { status = "ready", success = 42 }));

        var client = ClientTestFactory.CreateMessageClient(handler);
        var date = new DateTime(2024, 2, 1);

        var result = await client.GetNumberOfSentPushMessagesAsync(date);

        Assert.True(result.IsSuccess);
        Assert.Equal("ready", result.SuccessValue?.Status);
        Assert.Equal(42, result.SuccessValue?.Success);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/message/delivery/push", request.RequestUri?.AbsolutePath);
        Assert.Equal("date=20240201", request.RequestUri?.Query.TrimStart('?'));
    }
}
