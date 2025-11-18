using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class MessageAction : LabelableAction
    {
        public string Text { get; }

        public MessageAction(
            string label,
            string text) : base(label, ActionType.Message)
        {
            Text = text;
        }
    }
}
