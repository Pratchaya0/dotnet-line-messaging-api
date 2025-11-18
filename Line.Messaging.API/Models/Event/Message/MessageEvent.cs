using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class MessageEvent : ReplyableEvent
    {
        public MessageEventMessage Message { get; }

        public MessageEvent(
            MessageEventMessage message,
            string replyToken,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(replyToken, EventType.Message, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
            Message = message;
        }
    }
}
