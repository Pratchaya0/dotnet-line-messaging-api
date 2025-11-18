using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    /// <summary>
    /// Mention imprementation
    /// </summary>
    public class TextMessage : MessageEventMessage
    {
        public string Text { get; }
        public string QuoteToken { get; }

        public TextMessage(
            string id,
            string markAsReadToken,
            string quoteToken,
            string text)
            : base(id, MessageType.Text, markAsReadToken)
        {
            Text = text;
            QuoteToken = quoteToken;
        }
    }
}
