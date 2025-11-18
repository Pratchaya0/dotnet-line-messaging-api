using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Text
{
    public class TextMessage : BaseMessage
    {
        public string Text { get; }
        public string? QuoteToken { get; }
        public TextMessageEmoji? Emoji { get; }

        public TextMessage(
            string text,
            string? quoteToken = null,
            TextMessageEmoji? emoji = null,
            MessageQuickReply? quickReply = null) : base(MessageType.Text, quickReply)
        {
            Text = text;
            QuoteToken = quoteToken;
            Emoji = emoji;
        }
    }
}
