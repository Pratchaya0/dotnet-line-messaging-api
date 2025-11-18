using System;
using System.Net;
using System.Threading.Tasks;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients;

public class InsightClientTests
{
    [Fact]
    public async Task GetNumberOfMessageDeliveriesAsync_WhenSuccess_UsesDateQuery()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new { status = "ready", broadcast = 100 }));

        var client = ClientTestFactory.CreateInsightClient(handler);
        var date = new DateTime(2024, 3, 10);

        var result = await client.GetNumberOfMessageDeliveriesAsync(date);

        Assert.True(result.IsSuccess);
        Assert.Equal("ready", result.SuccessValue?.Status);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/insight/message/delivery", request.RequestUri?.AbsolutePath);
        Assert.Equal("date=20240310", request.RequestUri?.Query.TrimStart('?'));
    }

    [Fact]
    public async Task GetUserInteractionStatisticsAsync_WhenSuccess_AddsRequestIdQuery()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new
                {
                    overview = new
                    {
                        requestId = "req-1",
                        timestamp = 1,
                        delivered = 1,
                        uniqueImpressions = 1,
                        uniqueClick = 0,
                        uniqueMediaPlayed = 0,
                        uniqueMediaPlayed100Percent = 0
                    },
                    messages = Array.Empty<object>(),
                    clicks = Array.Empty<object>()
                }));

        var client = ClientTestFactory.CreateInsightClient(handler);
        var requestId = Guid.NewGuid();

        var result = await client.GetUserInteractionStatisticsAsync(requestId);

        Assert.True(result.IsSuccess);
        Assert.Equal("req-1", result.SuccessValue?.Overview?.RequestId);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/insight/message/event", request.RequestUri?.AbsolutePath);
        Assert.Equal($"requestId={requestId}", request.RequestUri?.Query.TrimStart('?'));
    }
}
