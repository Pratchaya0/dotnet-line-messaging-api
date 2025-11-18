using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class ClipboardAction : LabelableAction
    {
        public string ClipboardText { get; }

        public ClipboardAction(
            string label,
            string clipboardText)
            : base(label, ActionType.Clipboard)
        {
            ClipboardText = clipboardText;
        }
    }
}
