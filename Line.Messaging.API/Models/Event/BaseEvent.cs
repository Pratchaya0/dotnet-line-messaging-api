using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Event.AccountLink;
using Line.Messaging.API.Models.Event.Follow;
using Line.Messaging.API.Models.Event.Join;
using Line.Messaging.API.Models.Event.Message;
using Line.Messaging.API.Models.Event.Postback;
using Line.Messaging.API.Models.Event.Unfollow;
using Line.Messaging.API.Models.Event.Unsend;
using Line.Messaging.API.Models.Event.VideoPlayComplete;

namespace Line.Messaging.API.Models.Event
{
    public abstract class BaseEvent
    {
        public EventType Type { get; }
        public string Mode { get; }
        public EventSource? Source { get; }
        public long Timestamp { get; }
        public string WebhookEventId { get; }
        public EventDeliveryContext EventDeliveryContext { get; }

        protected BaseEvent(
            EventType type,
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext)
        {
            Type = type;
            Mode = mode;
            Source = source;
            Timestamp = timestamp;
            WebhookEventId = webhookEventId;
            EventDeliveryContext = eventDeliveryContext;
        }

        internal static BaseEvent Create(dynamic evt)
        {
            long timestamp = (long)evt.timestamp;
            string webhookEventId = (string)evt.webhookEventId;
            string mode = (string)evt.mode;

            EventType type = Enum.TryParse((string)evt.type, true, out EventType parsedType) ? parsedType : EventType.Message;

            EventSource source = EventSource.Create(evt.source);

            EventDeliveryContext deliveryContext = EventDeliveryContext.Create(evt.deliveryContext);

            switch (type)
            {
                case EventType.Message:
                    MessageEventMessage message = MessageEventMessage.Create(evt);
                    return new MessageEvent(
                        message, (string)evt.replyToken, mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.Unsend:
                    UnsendEventUnsend unsend = UnsendEventUnsend.Create(evt);
                    return new UnsendEvent(unsend, mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.Follow:
                    FollowEventFollow follow = FollowEventFollow.Create(evt);
                    return new FollowEvent(follow, (string)evt.replyToken, mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.Unfollow:
                    return new UnfollowEvent(mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.Join:
                    return new JoinEvent((string)evt.replyToken, mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.Postback:
                    return new PostbackEvent((string)evt.replyToken, mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.VideoPlayComplete:
                    VideoPlayCompleteEventVideoPlayComplete videoPlayComplete = VideoPlayCompleteEventVideoPlayComplete.Create(evt);
                    return new VideoPlayCompleteEvent(
                        videoPlayComplete, (string)evt.replyToken, mode, source, timestamp, webhookEventId, deliveryContext);

                case EventType.AccountLink:
                    AccountLinkEventLink accountLink = AccountLinkEventLink.Create(evt);
                    return new AccountLinkEvent(
                        mode, source, timestamp, webhookEventId, deliveryContext, accountLink, (string)evt.replyToken);

                case EventType.Membership:
                case EventType.MemberJoined:
                case EventType.MemberLeft:
                default:
                    throw new NotImplementedException($"Event type '{type}' is not implemented.");
            }
        }
    }
}
