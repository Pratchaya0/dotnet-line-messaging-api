using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Template.Template;

namespace Line.Messaging.API.Models.Message.Template
{
    public class TemplateMessage : BaseMessage
    {
        public string AltText { get; }
        public TemplateMessageTemplate Template { get; }

        public TemplateMessage(
            string altText,
            TemplateMessageTemplate template,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Template, quickReply)
        {
            AltText = altText;
            Template = template;
        }
    }
}
