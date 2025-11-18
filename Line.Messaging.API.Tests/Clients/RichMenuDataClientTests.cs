using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Tests.TestUtilities;
using Xunit;

namespace Line.Messaging.API.Tests.Clients;

public class RichMenuDataClientTests
{
    [Fact]
    public async Task UploadRichMenuImageAsync_WhenSuccess_SendsBinaryBody()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
            CapturingHttpMessageHandler.CreateJsonResponse(HttpStatusCode.OK, new { }));

        var client = ClientTestFactory.CreateRichMenuDataClient(handler);

        await using var stream = new MemoryStream(Encoding.UTF8.GetBytes("img-bytes"));
        var result = await client.UploadRichMenuImageAsync("rich-1", stream, "image/png");

        Assert.True(result.IsSuccess);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/richmenu/rich-1/content", request.RequestUri?.AbsolutePath);
        Assert.Contains("image/png", request.Headers["Content-Type"]);
        Assert.False(string.IsNullOrEmpty(request.Body));
    }

    [Fact]
    public async Task DownloadRichMenuImageAsync_WhenSuccess_ReturnsRawBytes()
    {
        var handler = new CapturingHttpMessageHandler(_ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(new byte[] { 1, 2, 3 })
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return response;
        });

        var client = ClientTestFactory.CreateRichMenuDataClient(handler);
        var result = await client.DownloadRichMenuImageAsync("rich-1");

        Assert.True(result.IsSuccess);
        Assert.Equal(new byte[] { 1, 2, 3 }, result.SuccessValue);

        var request = handler.SingleRequest;
        Assert.Equal("/v2/bot/richmenu/rich-1/content", request.RequestUri?.AbsolutePath);
        Assert.Equal(HttpMethod.Get, request.Method);
    }
}
