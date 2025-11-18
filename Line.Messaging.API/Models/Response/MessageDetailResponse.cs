using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class MessageDetailResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("details")]
        public List<MessageDetail>? Details { get; set; }
    }

    public class MessageDetail
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("property")]
        public string Property { get; set; }
    }
}
