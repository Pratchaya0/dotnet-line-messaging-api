using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public enum MessageType
    {
        Text,
        Image,
        Video,
        Audio,
        File,
        Location,
        Sticker
    }
}
