using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap.Action
{
    public class MessageAction : ImagemapMessageActions
    {
        public string Text { get; }
        public string? Label { get; }
        public MessageAction(
            ActionArea area,
            string text,
            string? label = null)
            : base(ActionType.Message, area)
        {
            Text = text;
            Label = label;
        }
    }
}
