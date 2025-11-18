using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Location
{
    public class LocationMessage : BaseMessage
    {
        public string Title { get; }
        public string Address { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }

        public LocationMessage(
            string title,
            string address,
            decimal latitude,
            decimal longitude,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Location, quickReply)
        {
            Title = title;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
