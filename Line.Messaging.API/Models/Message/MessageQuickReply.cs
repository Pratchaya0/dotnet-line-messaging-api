using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message
{
    public class MessageQuickReply
    {
        public IEnumerable<MessageQuickReplyItem> Items { get; }

        public MessageQuickReply(IEnumerable<MessageQuickReplyItem> items)
        {
            Items = items;
        }

        internal static MessageQuickReply Create(dynamic quickReply)
        {
            var items = new List<MessageQuickReplyItem>();
            foreach (var item in quickReply.items)
            {
                items.Add(MessageQuickReplyItem.Create(item));
            }
            return new MessageQuickReply(items);
        }
    }
}
