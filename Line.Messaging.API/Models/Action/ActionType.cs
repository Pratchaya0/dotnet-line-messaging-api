using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Action
{
    public enum ActionType
    {
        Postback,
        Message,
        Uri,
        DatetimePicker,
        Camera,
        CameraRoll,
        Location,
        RichmenuSwitch,
        Clipboard
    }
}
