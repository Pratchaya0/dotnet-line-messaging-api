using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class MediaMessage : MessageEventMessage
    {
        public ContentProvider ContentProvider { get; }
        public int Duration { get; }
        public string QuoteToken { get; }

        public MediaMessage(
            string id,
            MessageType type,
            string markAsReadToken,
            ContentProvider contentProvider,
            int duration,
            string quoteToken) : base(id, type, markAsReadToken)
        {
            ContentProvider = contentProvider;
            Duration = duration;
            QuoteToken = quoteToken;
        }
    }
}
