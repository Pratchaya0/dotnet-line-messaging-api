using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class AudioMessage : MessageEventMessage
    {
        public ContentProvider ContentProvider { get; }
        public int Duration { get; }

        public AudioMessage(
            string id,
            string markAsReadToken,
            ContentProvider contentProvider,
            int duration) : base(id, MessageType.Audio, markAsReadToken)
        {
            ContentProvider = contentProvider;
            Duration = duration;
        }
    }
}
