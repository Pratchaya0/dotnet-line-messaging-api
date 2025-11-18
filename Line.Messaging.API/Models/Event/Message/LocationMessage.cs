using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class LocationMessage : MessageEventMessage
    {
        public string? Title { get; }
        public string? Address { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public LocationMessage(
                string id,
                string markAsReadToken,
                decimal latitude,
                decimal longitude,
                string? title = null,
                string? address = null
            ) : base(id, MessageType.Location, markAsReadToken)
        {
            Title = title;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
