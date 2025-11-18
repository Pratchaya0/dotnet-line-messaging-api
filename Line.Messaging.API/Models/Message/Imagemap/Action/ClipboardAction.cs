using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap.Action
{
    public class ClipboardAction : ImagemapMessageActions
    {
        public string ClipboardText { get; }
        public string? Label { get; }
        public ClipboardAction(
            ActionArea area,
            string clipboardText,
            string? label = null)
            : base(ActionType.Clipboard, area)
        {
            ClipboardText = clipboardText;
            Label = label;
        }
    }
}
