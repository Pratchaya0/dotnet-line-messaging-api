using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event
{
    public class EventSource
    {
        public EventSourceType Type { get; set; }
        public string UserId { get; set; }

        public EventSource(EventSourceType type, string userId)
        {
            Type = type;
            UserId = userId;
        }

        internal static EventSource Create(dynamic source)
        {
            string userId = source.userId;

            if (!Enum.TryParse((string)source.type, true, out EventSourceType type))
            {
                return new EventSource(EventSourceType.User, userId);
            }

            return new EventSource(type, userId);
        }
    }
}
