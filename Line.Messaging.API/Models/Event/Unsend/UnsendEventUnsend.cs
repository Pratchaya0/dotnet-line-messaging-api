using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Unsend
{
    public class UnsendEventUnsend
    {
        public string MessageId { get; }
        public UnsendEventUnsend(string messageId)
        {
            MessageId = messageId;
        }

        internal static UnsendEventUnsend Create(dynamic unsend)
        {
            return new UnsendEventUnsend((string)unsend.messageId);
        }
    }
}
