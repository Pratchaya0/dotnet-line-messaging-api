using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class FriendDemographicsResponse
    {
        [JsonPropertyName("available")]
        public bool Available { get; set; }

        [JsonPropertyName("genders")]
        public List<GenderDetail> Genders { get; set; }

        [JsonPropertyName("ages")]
        public List<AgeDetail> Aages { get; set; }

        [JsonPropertyName("areas")]
        public List<AreaDetail> Areas { get; set; }

        [JsonPropertyName("appTypes")]
        public List<AppTypeDetail> AppTypes { get; set; }

        [JsonPropertyName("subscriptionPeriods")]
        public List<SubscriptionPeriodDetail> SubscriptionPeriods { get; set; }
    }

    public class GenderDetail
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("percentage")]
        public string Percentage { get; set; }
    }

    public class AgeDetail
    {
        [JsonPropertyName("age")]
        public string Age { get; set; }

        [JsonPropertyName("percentage")]
        public int Percentage { get; set; }
    }

    public class AreaDetail
    {
        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("percentage")]
        public int Percentage { get; set; }
    }

    public class AppTypeDetail
    {
        [JsonPropertyName("appType")]
        public string AppType { get; set; }

        [JsonPropertyName("percentage")]
        public int Percentage { get; set; }
    }

    public class SubscriptionPeriodDetail
    {
        [JsonPropertyName("subscriptionPeriod")]
        public string SubscriptionPeriod { get; set; }

        [JsonPropertyName("subscriptionPeriod")]
        public int Percentage { get; set; }
    }
}

