using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message
{
    public class MessageQuickReplyItem
    {
        public string Type { get; } = "action";
        public ActionAction Action { get; }
        public string? ImageUrl { get; }

        public MessageQuickReplyItem(
            string type,
            ActionAction action,
            string? imageUrl = null)
        {
            Type = type;
            Action = action;
            ImageUrl = imageUrl;
        }

        internal static MessageQuickReplyItem Create(dynamic quickReplyItem)
        {
            string type = (string)quickReplyItem.type;

            ActionAction action = ActionAction.Create(quickReplyItem.action);

            string? imageUrl = quickReplyItem.imageUrl != null ? (string)quickReplyItem.imageUrl : null;

            return new MessageQuickReplyItem(
                type,
                action,
                imageUrl);
        }
    }
}
