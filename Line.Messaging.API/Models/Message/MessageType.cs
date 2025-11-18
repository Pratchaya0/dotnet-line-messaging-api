using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message
{
    public enum MessageType
    {
        Text,
        TextV2,
        Sticker,
        Image,
        Video,
        Audio,
        Location,
        Coupon,
        Imagemap,
        Template,
        Flex,
    }
}
