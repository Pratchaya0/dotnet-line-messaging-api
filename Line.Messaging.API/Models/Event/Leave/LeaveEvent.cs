using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Leave
{
    public class LeaveEvent : BaseEvent
    {
        public LeaveEvent(
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(EventType.Leave, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
        }
    }
}
