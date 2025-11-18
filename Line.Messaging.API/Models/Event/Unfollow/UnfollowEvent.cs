using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Unfollow
{
    public class UnfollowEvent : BaseEvent
    {
        public UnfollowEvent(
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(EventType.Unfollow, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
        }
    }
}
