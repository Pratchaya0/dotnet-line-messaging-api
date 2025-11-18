using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class SentMessagesResponse
    {
        [JsonPropertyName("sentMessages")]
        public List<SentMessage> SentMessages { get; set; } = new();
    }

    public class SentMessage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("quoteToken")]
        public string QuoteToken { get; set; } = string.Empty;
    }
}
