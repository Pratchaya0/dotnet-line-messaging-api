using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Image
{
    public class ImageMessage : BaseMessage
    {
        public string OriginalContentUrl { get; }
        public string PreviewImageUrl { get; }
        public ImageMessage(
            string originalContentUrl,
            string previewImageUrl,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Image, quickReply)
        {
            OriginalContentUrl = originalContentUrl;
            PreviewImageUrl = previewImageUrl;
        }
    }
}
