using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event
{
    public class ReplyableEvent : BaseEvent
    {
        public string? ReplyToken { get; }

        public ReplyableEvent(
            string replyToken,
            EventType type,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(type, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
            ReplyToken = replyToken;
        }
    }
}
