using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class PhaseResponse
    {
        [JsonPropertyName("phase")]
        public string Phase { get; set; }

        [JsonPropertyName("acceptedTime")]
        public DateTime AcceptedTime { get; set; }

        [JsonPropertyName("completedTime")]
        public DateTime CompletedTime { get; set; }
    }
}
