using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Template.Confirm
{
    public class ConfirmTemplate : TemplateMessageTemplate
    {
        public string Text { get; }
        public IEnumerable<ActionAction> Actions { get; }

        public ConfirmTemplate(
            string text,
            IEnumerable<ActionAction> actions)
            : base(TemplateMessageTemplateType.Confirm)
        {
            Text = text;
            Actions = actions;
        }
    }
}
