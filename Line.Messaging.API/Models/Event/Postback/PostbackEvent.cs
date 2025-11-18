using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Postback
{
    public class PostbackEvent : ReplyableEvent
    {
        public PostbackEvent(
            string replyToken,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(replyToken, EventType.Postback, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
        }
    }
}


