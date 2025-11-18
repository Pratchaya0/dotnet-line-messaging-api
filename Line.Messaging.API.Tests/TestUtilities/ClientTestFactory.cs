using System;
using System.Net.Http;
using System.Reflection;
using Line.Messaging.API.Clients.Message;
using Line.Messaging.API.Common;
using RestSharp;

namespace Line.Messaging.API.Tests.TestUtilities;

internal static class ClientTestFactory
{
    private const string DefaultInstanceUrl = DomainName.OTHER_ENDPOINT;
    private const string DefaultToken = "test-channel-token";

    private static readonly FieldInfo ClientField = typeof(MessagingAPIClient)
        .GetField("<Client>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
        ?? throw new InvalidOperationException("Unable to access MessagingAPIClient.Client via reflection.");

    public static MessageClient CreateMessageClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new MessageClient(DefaultInstanceUrl, DefaultToken), handler);

    public static UserClient CreateUserClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new UserClient(DefaultInstanceUrl, DefaultToken), handler);

    public static RichMenuClient CreateRichMenuClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new RichMenuClient(DefaultInstanceUrl, DefaultToken), handler);

    private static TClient CreateClient<TClient>(Func<TClient> factory, HttpMessageHandler handler)
        where TClient : MessagingAPIClient
    {
        var client = factory();

        var restClient = new RestClient(new RestClientOptions(DefaultInstanceUrl)
        {
            ConfigureMessageHandler = _ => handler,
            ThrowOnAnyError = true,
        });

        ClientField.SetValue(client, restClient);
        return client;
    }
}
