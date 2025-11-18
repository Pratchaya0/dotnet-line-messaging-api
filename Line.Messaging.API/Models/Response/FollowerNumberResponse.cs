using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class FollowerNumberResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("followers")]
        public int? Followers { get; set; }

        [JsonPropertyName("targetedReaches")]
        public int? TargetedReaches { get; set; }

        [JsonPropertyName("blocks")]
        public int? Blocks { get; set; }
    }
}
