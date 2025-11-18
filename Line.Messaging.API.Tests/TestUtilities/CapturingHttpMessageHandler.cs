using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Line.Messaging.API.Tests.TestUtilities;

internal sealed class CapturingHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _responseFactory;
    private readonly List<CapturedRequest> _requests = new();

    public CapturingHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
    {
        _responseFactory = responseFactory;
    }

    public IReadOnlyList<CapturedRequest> Requests => _requests;

    public CapturedRequest SingleRequest =>
        _requests.Count == 1
            ? _requests[0]
            : throw new InvalidOperationException($"Expected exactly one request but found {_requests.Count}.");

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string? body = null;
        if (request.Content is not null)
        {
            body = await request.Content.ReadAsStringAsync(cancellationToken);
        }

        var headers = request.Headers
            .Concat(request.Content?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>())
            .ToDictionary(
                pair => pair.Key,
                pair => string.Join(",", pair.Value),
                StringComparer.OrdinalIgnoreCase);

        _requests.Add(new CapturedRequest(request.Method, request.RequestUri, body, headers));

        return _responseFactory(request);
    }

    public static HttpResponseMessage CreateJsonResponse<T>(
        HttpStatusCode statusCode,
        T body,
        IDictionary<string, string>? headers = null)
    {
        var content = JsonSerializer.Serialize(body);
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        if (headers is not null)
        {
            foreach (var header in headers)
            {
                response.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        return response;
    }
}
