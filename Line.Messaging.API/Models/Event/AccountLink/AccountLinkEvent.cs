using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.AccountLink
{
    public class AccountLinkEvent : BaseEvent
    {
        public string? ReplyToken { get; }
        public AccountLinkEventLink Link { get; }
        public AccountLinkEvent(
            string mode,
            EventSource source,
            long timestamp,
            string webhookEventId,
            EventDeliveryContext eventDeliveryContext,
            AccountLinkEventLink link,
            string? replyToken = null)
            : base(EventType.AccountLink, mode, source, timestamp, webhookEventId, eventDeliveryContext)
        {
            ReplyToken = replyToken;
            Link = link;
        }
    }
}
