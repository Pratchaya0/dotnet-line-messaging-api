using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Template
{
    public abstract class TemplateMessageTemplate
    {
        public TemplateMessageTemplateType Type { get; }

        protected TemplateMessageTemplate(TemplateMessageTemplateType type)
        {
            Type = type;
        }

        internal static TemplateMessageTemplate Create(dynamic template)
        {
            if (!Enum.TryParse((string)template.type, true, out TemplateMessageTemplateType type))
            {
                throw new InvalidCastException($"Template type '{template.type}' is invalid.");
            }

            throw new NotImplementedException();
        }
    }
}
