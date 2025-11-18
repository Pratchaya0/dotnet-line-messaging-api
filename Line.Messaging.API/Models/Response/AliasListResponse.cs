using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class AliasListResponse
    {
        [JsonPropertyName("aliases")]
        public List<AliasResponse> Aliases { get; set; }
    }
}
