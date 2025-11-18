using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Event;

namespace Line.Messaging.API.Models.Webhook
{
    public class WebhookEvent
    {
        public string Destination { get; }
        public IEnumerable<Models.Event.BaseEvent?> Events { get; }

        public WebhookEvent(
            string destination,
            IEnumerable<Models.Event.BaseEvent?> events)
        {
            Destination = destination;
            Events = events;
        }
    }
}
