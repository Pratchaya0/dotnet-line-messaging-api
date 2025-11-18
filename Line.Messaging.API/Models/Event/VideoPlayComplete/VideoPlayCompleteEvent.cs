using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.VideoPlayComplete
{
    public class VideoPlayCompleteEvent : ReplyableEvent
    {
        public VideoPlayCompleteEventVideoPlayComplete VideoPlayComplete { get; }
        public VideoPlayCompleteEvent(
            VideoPlayCompleteEventVideoPlayComplete videoPlayComplete,
            string replyToken,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
            : base(replyToken, EventType.VideoPlayComplete, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
            VideoPlayComplete = videoPlayComplete;
        }
    }
}
