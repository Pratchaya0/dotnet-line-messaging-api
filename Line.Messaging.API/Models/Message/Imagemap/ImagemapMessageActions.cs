using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Imagemap.Action;

namespace Line.Messaging.API.Models.Message.Imagemap
{
    public abstract class ImagemapMessageActions
    {
        public ActionType Type { get; }
        public ActionArea Area { get; }

        protected ImagemapMessageActions(ActionType type, ActionArea area)
        {
            Type = type;
            Area = area;
        }

        internal static ImagemapMessageActions Create(dynamic actions)
        {
            if (!Enum.TryParse((string)actions.type, true, out ActionType type))
            {
                throw new InvalidCastException($"Imagemap action type '{actions.type}' is invalid.");
            }

            ActionArea area = ActionArea.Create(actions.area);

            switch (type)
            {
                case ActionType.Uri:
                    return new UriAction(area, (string)actions.linkUri, (string?)actions?.label);

                case ActionType.Message:
                    return new MessageAction(area, (string)actions.text, (string?)actions?.label);

                case ActionType.Clipboard:
                    return new ClipboardAction(area, (string)actions.clipboardText, (string?)actions?.label);

                default:
                    throw new NotSupportedException($"Imagemap action type '{type}' is not supported.");
            }
        }
    }
}
