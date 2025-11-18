using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Join
{
    public class JoinEvent : ReplyableEvent
    {
        public JoinEvent(
            string replyToken,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(replyToken, EventType.Join, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
        }
    }
}
