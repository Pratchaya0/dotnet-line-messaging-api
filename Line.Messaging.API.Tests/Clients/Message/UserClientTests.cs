using System.Net;
using System.Threading.Tasks;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients.Message;

public class UserClientTests
{
    [Fact]
    public async Task GetProfileAsync_WhenRequestSucceeds_ReturnsProfile()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new
                {
                    displayName = "Alice",
                    userId = "U123",
                    statusMessage = "Hello"
                }));

        var client = ClientTestFactory.CreateUserClient(handler);

        var result = await client.GetProfileAsync("U123");

        Assert.True(result.IsSuccess);
        Assert.Equal("Alice", result.SuccessValue?.DisplayName);
        Assert.Equal("U123", result.SuccessValue?.UserId);
        Assert.Equal("Hello", result.SuccessValue?.StatusMessage);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/profile/U123", request.RequestUri?.AbsolutePath);
        Assert.Equal("Bearer test-channel-token", request.Headers["Authorization"]);
    }

    [Fact]
    public async Task GetFollowersAsync_WhenRequestSucceeds_AppliesQueryParameters()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(
                HttpStatusCode.OK,
                new
                {
                    userIds = new[] { "U1", "U2" },
                    next = "token"
                }));

        var client = ClientTestFactory.CreateUserClient(handler);

        var result = await client.GetAListOfUsersWhoAddedYourLineOfficialAccountAsAFriendAsync(limit: 500, start: "token");

        Assert.True(result.IsSuccess);
        Assert.Equal(new[] { "U1", "U2" }, result.SuccessValue?.UserIds);
        Assert.Equal("token", result.SuccessValue?.Next);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/followers/ids", request.RequestUri?.AbsolutePath);
        Assert.Contains("limit=500", request.RequestUri?.Query);
        Assert.Contains("start=token", request.RequestUri?.Query);
    }
}
