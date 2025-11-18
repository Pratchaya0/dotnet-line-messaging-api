using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Unsend
{
    public class UnsendEvent : BaseEvent
    {
        public UnsendEventUnsend Unsend { get; }

        public UnsendEvent(
            UnsendEventUnsend unsend,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(EventType.Unsend, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
            Unsend = unsend;
        }
    }
}
