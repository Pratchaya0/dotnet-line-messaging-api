using System;
using System.Net.Http;
using System.Reflection;
using Line.Messaging.API.Clients;
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
    
    public static InsightClient CreateInsightClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new InsightClient(DefaultInstanceUrl, DefaultToken), handler);

    public static UserClient CreateUserClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new UserClient(DefaultInstanceUrl, DefaultToken), handler);

    public static RichMenuClient CreateRichMenuClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new RichMenuClient(DefaultInstanceUrl, DefaultToken), handler);

    public static RichMenuAliasClient CreateRichMenuAliasClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new RichMenuAliasClient(DefaultInstanceUrl, DefaultToken), handler);

    public static PerUserRichMenuClient CreatePerUserRichMenuClient(CapturingHttpMessageHandler handler) =>
        CreateClient(() => new PerUserRichMenuClient(DefaultInstanceUrl, DefaultToken), handler);

    public static RichMenuDataClient CreateRichMenuDataClient(CapturingHttpMessageHandler handler) =>
        CreateClient(
            () => new RichMenuDataClient(DomainName.DATA_ENDPOINT, DefaultToken),
            handler,
            DomainName.DATA_ENDPOINT);

    private static TClient CreateClient<TClient>(
        Func<TClient> factory,
        HttpMessageHandler handler,
        string baseUrl = DefaultInstanceUrl)
        where TClient : MessagingAPIClient
    {
        var client = factory();

        var restClient = new RestClient(new RestClientOptions(baseUrl)
        {
            ConfigureMessageHandler = _ => handler,
            ThrowOnAnyError = true,
        });

        ClientField.SetValue(client, restClient);
        return client;
    }
}
