using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Video
{
    public class VideoMessage : BaseMessage
    {
        public string OriginalContentUrl { get; }
        public string PreviewImageUrl { get; }
        public string? TrackingId { get; }
        public VideoMessage(
            string originalContentUrl,
            string previewImageUrl,
            string? trackingId = null,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Video, quickReply)
        {
            OriginalContentUrl = originalContentUrl;
            PreviewImageUrl = previewImageUrl;
            TrackingId = trackingId;
        }
    }
}
