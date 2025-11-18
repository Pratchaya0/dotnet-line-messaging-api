using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class InteractionStatisticResponse
    {
        [JsonPropertyName("overview")]
        public OverviewDetail Overview { get; set; }

        [JsonPropertyName("messages")]
        public List<InteractionStatisticMessage> Messages { get; set; }

        [JsonPropertyName("clicks")]
        public List<InteractionStatisticClick> Clicks { get; set; }
    }

    public class OverviewDetail
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        [JsonPropertyName("delivered")]
        public int Delivered { get; set; }

        [JsonPropertyName("uniqueImpressions")]
        public int UniqueImpression { get; set; }

        [JsonPropertyName("uniqueClick")]
        public int UniqueClick { get; set; }

        [JsonPropertyName("uniqueMediaPlayed")]
        public int UniqueMediaPlayed { get; set; }

        [JsonPropertyName("uniqueMediaPlayed100Percent")]
        public int UniqueMediaPlayed100Percent { get; set; }
    }

    public class InteractionStatisticMessage
    {
        [JsonPropertyName("seq")]
        public int Seq { get; set; }

        [JsonPropertyName("impression")]
        public int Impression { get; set; }

        [JsonPropertyName("mediaPlayed")]
        public int MediaPlayed { get; set; }

        [JsonPropertyName("mediaPlayed25Percent")]
        public int MediaPlayed25Percent { get; set; }

        [JsonPropertyName("mediaPlayed50Percent")]
        public int MediaPlayed50Percent { get; set; }

        [JsonPropertyName("mediaPlayed75Percent")]
        public int MediaPlayed75Percent { get; set; }

        [JsonPropertyName("mediaPlayed100Percent")]
        public int MediaPlayed100Percent { get; set; }

        [JsonPropertyName("uniqueMediaPlayed")]
        public int UniqueMediaPlayed { get; set; }

        [JsonPropertyName("uniqueMediaPlayed25Percent")]
        public int UniqueMediaPlayed25Percent { get; set; }

        [JsonPropertyName("uniqueMediaPlayed50Percent")]
        public int UniqueMediaPlayed50Percent { get; set; }

        [JsonPropertyName("uniqueMediaPlayed75Percent")]
        public int UniqueMediaPlayed75Percent { get; set; }

        [JsonPropertyName("uniqueMediaPlayed100Percent")]
        public int UniqueMediaPlayed100Percent { get; set; }
    }

    public class InteractionStatisticClick
    {
        [JsonPropertyName("seq")]
        public int Seq { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("click")]
        public int Click { get; set; }

        [JsonPropertyName("uniqueClick")]
        public int UniqueClick { get; set; }

        [JsonPropertyName("uniqueClickOfRequest")]
        public int UniqueClickOfRequest { get; set; }
    }
}
