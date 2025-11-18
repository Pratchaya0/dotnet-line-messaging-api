using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;
using Line.Messaging.API.Models.Message.Action;
using Line.Messaging.API.Models.RichMenu;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients.Message;

public class RichMenuClientTests
{
    [Fact]
    public async Task CreateRichMenuAsync_WhenRequestSucceeds_SerializesRichMenu()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new { richMenuId = "rich-1" }));

        var client = ClientTestFactory.CreateRichMenuClient(handler);
        var richMenu = new TestRichMenu();

        var result = await client.CreateRichMenuAsync(richMenu);

        Assert.True(result.IsSuccess);
        Assert.Equal("rich-1", result.SuccessValue?.RichMenuId);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/richmenu", request.RequestUri?.AbsolutePath);

        Assert.False(string.IsNullOrWhiteSpace(request.Body), "Captured request body was empty.");

        using var payload = JsonDocument.Parse(request.Body!);
        Assert.True(payload.RootElement.TryGetProperty("name", out var name), request.Body);
        Assert.Equal("Main Menu", name.GetString());
        Assert.True(payload.RootElement.TryGetProperty("chatBarText", out var chatBarText), request.Body);
        Assert.Equal("Tap here", chatBarText.GetString());
        Assert.True(payload.RootElement.TryGetProperty("size", out var size), request.Body);
        Assert.Equal(2500, size.GetProperty("width").GetInt32());
        Assert.True(payload.RootElement.TryGetProperty("areas", out var areasElement), request.Body);
        Assert.True(areasElement.GetArrayLength() > 0, request.Body);
        var firstArea = areasElement[0];
        Assert.True(firstArea.TryGetProperty("action", out var action), request.Body);
        Assert.True(firstArea.TryGetProperty("bounds", out var bounds), request.Body);
        Assert.Equal(1250, bounds.GetProperty("width").GetInt32());
        Assert.True(action.TryGetProperty("type", out var actionType), request.Body);
        Assert.Equal((int)ActionType.Message, actionType.GetInt32());
    }

    private sealed class TestRichMenu : BaseRichMenu
    {
        public TestRichMenu()
            : base(
                new RichMenuSize(2500, 1686),
                true,
                "Main Menu",
                "Tap here",
                new List<RichMenuArea>
                {
                    new(
                        new RichMenuAreaBound(0, 0, 1250, 843),
                        new MessageAction("Buy", "Buy now"))
                })
        {
        }
    }
}
