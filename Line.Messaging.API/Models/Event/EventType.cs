using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event
{
    public enum EventType
    {
        Message,
        Unsend,
        Follow,
        Unfollow,
        Join,
        Leave,
        MemberJoined,
        MemberLeft,
        Postback,
        VideoPlayComplete,
        Beacon,
        AccountLink,
        Membership
    }
}
