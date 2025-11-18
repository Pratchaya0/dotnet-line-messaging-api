using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Audio
{
    public class AudioMessage : BaseMessage
    {
        public string OriginalContentUrl { get; }
        public int Duration { get; }
        public AudioMessage(
            string originalContentUrl,
            int duration,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Audio, quickReply)
        {
            OriginalContentUrl = originalContentUrl;
            Duration = duration;
        }
    }
}
