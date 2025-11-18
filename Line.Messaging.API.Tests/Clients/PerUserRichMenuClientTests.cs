using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients;

public class PerUserRichMenuClientTests
{
    [Fact]
    public async Task LinkRichMenuToUserAsync_WhenSuccess_UsesUrlSegments()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(HttpStatusCode.OK, new { }));

        var client = ClientTestFactory.CreatePerUserRichMenuClient(handler);

        var result = await client.LinkRichMenuToUserAsync("U123", "rich-1");

        Assert.True(result.IsSuccess);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/user/U123/richmenu/rich-1", request.RequestUri?.AbsolutePath);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("Bearer test-channel-token", request.Headers["Authorization"]);
    }

    [Fact]
    public async Task LinkRichMenuToMultipleUsersAsync_WhenAccepted_BuildsJsonPayload()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(HttpStatusCode.Accepted, new { }));

        var client = ClientTestFactory.CreatePerUserRichMenuClient(handler);

        var result = await client.LinkRichMenuToMultipleUsersAsync("rich-1", new List<string> { "U1", "U2" });

        Assert.True(result.IsSuccess);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/richmenu/bulk/link", request.RequestUri?.AbsolutePath);
        Assert.Contains("application/json", request.Headers["Content-Type"], StringComparison.OrdinalIgnoreCase);

        using var payload = JsonDocument.Parse(request.Body!);
        Assert.Equal("rich-1", payload.RootElement.GetProperty("richMenuId").GetString());
        Assert.Equal(new[] { "U1", "U2" }, payload.RootElement.GetProperty("userIds").Deserialize<string[]>());
    }
}
