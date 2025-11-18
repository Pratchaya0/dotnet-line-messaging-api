using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class ProfileResponse
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; } = null;

        [JsonPropertyName("pictureUrl")]
        public string? PictureUrl { get; set; } = null;

        [JsonPropertyName("statusMessage")]
        public string? StatusMessage { get; set; } = null;
    }
}
