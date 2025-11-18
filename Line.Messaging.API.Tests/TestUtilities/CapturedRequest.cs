using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Line.Messaging.API.Tests.TestUtilities;

internal sealed record CapturedRequest(
    HttpMethod Method,
    Uri? RequestUri,
    string? Body,
    IReadOnlyDictionary<string, string> Headers);
