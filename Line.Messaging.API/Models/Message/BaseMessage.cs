using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message
{
    public abstract class BaseMessage
    {
        public MessageType Type { get; }
        public MessageQuickReply? QuickReply { get; }

        public BaseMessage(MessageType type, MessageQuickReply? quickReply = null)
        {
            Type = type;
            QuickReply = quickReply;
        }

        internal static BaseMessage Create(dynamic message)
        {
            if (!Enum.TryParse((string)message.type, true, out MessageType type))
            {
                type = MessageType.Text;
            }

            MessageQuickReply? quickReply =
                message?.quickReply is not null
                    ? MessageQuickReply.Create(message.quickReply)
                    : null;

            throw new NotImplementedException($"Message type '{type}' is not implemented.");
        }
    }
}
