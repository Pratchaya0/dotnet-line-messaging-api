using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class HeaderResponse
    {
        [JsonPropertyName("X-Line-Request-Id")]
        public string XLineRequestId { get; set; }

        [JsonPropertyName("X-Line-Accepted-Request-Id")]
        public string? XLineAcceptedRequestId { get; set; }
    }
}
