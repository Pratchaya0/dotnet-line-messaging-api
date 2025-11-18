using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class AddedUserResponse
    {
        [JsonPropertyName("userIds")]
        public List<string> UserIds { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }
    }
}
