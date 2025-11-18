using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Follow
{
    public class FollowEvent : ReplyableEvent
    {
        public FollowEventFollow Follow { get; }

        public FollowEvent(
            FollowEventFollow follow,
            string replyToken,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(replyToken, EventType.Follow, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
            Follow = follow;
        }
    }
}
