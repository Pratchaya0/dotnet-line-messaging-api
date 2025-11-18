using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Beacon
{
    public class BeaconEvent : ReplyableEvent
    {
        public BeaconEvent(
            string replyToken,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(replyToken, EventType.Beacon, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
        }
    }
}
