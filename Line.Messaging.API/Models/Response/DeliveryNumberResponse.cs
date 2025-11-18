using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class DeliveryNumberResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("broadcast")]
        public int? Broadcast { get; set; }

        [JsonPropertyName("targeting")]
        public int? Targeting { get; set; }

        [JsonPropertyName("stepMessage")]
        public int? StepMessage { get; set; }

        [JsonPropertyName("autoResponse")]
        public int? AutoResponse { get; set; }

        [JsonPropertyName("welcomeResponse")]
        public int? WelcomeResponse { get; set; }

        [JsonPropertyName("chat")]
        public int? Chat { get; set; }

        [JsonPropertyName("apiBroadcast")]
        public int? ApiBroadcast { get; set; }

        [JsonPropertyName("apiPush")]
        public int? ApiPush { get; set; }

        [JsonPropertyName("apiMulticast")]
        public int? ApiMulticast { get; set; }

        [JsonPropertyName("apiNarrowcast")]
        public int? ApiNarrowcast { get; set; }

        [JsonPropertyName("apiReply")]
        public int? ApiReply { get; set; }

        [JsonPropertyName("ccAutoReply")]
        public int? CcAutoReply { get; set; }

        [JsonPropertyName("ccManualReply")]
        public int? CcManualReply { get; set; }

        [JsonPropertyName("pnpNoticeMessage")]
        public int? PnpNoticeMessage { get; set; }

        [JsonPropertyName("pnpCallToLine")]
        public int? PnpCallToLine { get; set; }

        [JsonPropertyName("thirdPartyChatTool")]
        public int? ThirdPartyChatTool { get; set; }
    }
}
