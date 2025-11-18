using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients;

public class RichMenuAliasClientTests
{
    [Fact]
    public async Task CreateRichMenuAliasAsync_WhenSuccess_SendsAliasPayload()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(HttpStatusCode.OK, new { }));

        var client = ClientTestFactory.CreateRichMenuAliasClient(handler);

        var result = await client.CreateRichMenuAliasAsync("alias-1", "rich-1");

        Assert.True(result.IsSuccess);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/richmenu/alias", request.RequestUri?.AbsolutePath);
        Assert.Contains("application/json", request.Headers["Content-Type"], StringComparison.OrdinalIgnoreCase);

        using var payload = JsonDocument.Parse(request.Body!);
        Assert.Equal("alias-1", payload.RootElement.GetProperty("richMenuAliasId").GetString());
        Assert.Equal("rich-1", payload.RootElement.GetProperty("richMenuId").GetString());
    }

    [Fact]
    public async Task GetRichMenuAliasInformationAsync_WhenSuccess_TargetsAliasEndpoint()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new { richMenuAliasId = "alias-1", richMenuId = "rich-1" }));

        var client = ClientTestFactory.CreateRichMenuAliasClient(handler);

        var result = await client.GetRichMenuAliasInformationAsync("alias-1");

        Assert.True(result.IsSuccess);
        Assert.Equal("alias-1", result.SuccessValue?.RichMenuAliasId);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/richmenu/alias/alias-1", request.RequestUri?.AbsolutePath);
        Assert.Equal("Bearer test-channel-token", request.Headers["Authorization"]);
    }
}
