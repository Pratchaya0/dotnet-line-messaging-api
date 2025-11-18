using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public abstract class ActionAction
    {
        public ActionType Type { get; }
        public ActionAction(ActionType type)
        {
            Type = type;
        }

        internal static ActionAction Create(dynamic action)
        {
            if (!Enum.TryParse((string)action.type, true, out ActionType type))
            {
                throw new InvalidCastException($"Action type '{action.type}' is invalid.");
            }

            switch (type)
            {
                case ActionType.Message:
                    return new MessageAction(
                        (string)action.label,
                        (string)action.text);
                case ActionType.Postback:
                    return new PostbackAction(
                        (string)action.label,
                        (string)action.data,
                        (string)action.fillInText,
                        (string?)action?.displayText,
                        (string?)action?.text,
                        (string?)action?.inputOption);
                case ActionType.Uri:
                    return new UriAction(
                        (string)action.label,
                        (string)action.uri);
                case ActionType.Camera:
                    return new CameraAction(
                        (string)action.label);
                case ActionType.CameraRoll:
                    return new CameraRollAction(
                        (string)action.label);
                case ActionType.Location:
                    return new LocationAction(
                        (string)action.label);
                case ActionType.DatetimePicker:
                    if (!Enum.TryParse((string)action.mode, true, out DatetimePickerActionMode mode))
                    {
                        mode = DatetimePickerActionMode.Datetime;
                    }
                    return new DatetimePickerAction(
                        (string)action.label,
                        (string)action.data,
                        mode,
                        (string?)action?.initial,
                        (string?)action?.max,
                        (string?)action?.min);
                case ActionType.RichmenuSwitch:
                    return new RichmenuSwitchAction(
                        (string)action.richMenuAliasId,
                        (string)action.data,
                        (string?)action?.label);
                case ActionType.Clipboard:
                    return new ClipboardAction(
                        (string)action.label,
                        (string)action.clipboardText);

                default:
                    throw new NotSupportedException($"Quick reply action type '{type}' is not supported.");
            }
        }
    }
}
