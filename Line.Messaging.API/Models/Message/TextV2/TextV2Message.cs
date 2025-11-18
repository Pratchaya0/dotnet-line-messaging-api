using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.TextV2
{
    public class TextV2Message : BaseMessage
    {
        public string Text { get; }
        public Dictionary<string, TextV2MessageSubstitution>? Substitution { get; }
        public TextV2Message(
            string text,
            Dictionary<string, TextV2MessageSubstitution>? substitution = null,
            MessageQuickReply? quickReply = null)
            : base(MessageType.TextV2, quickReply)
        {
            Text = text;
            Substitution = substitution;
        }
    }
}
