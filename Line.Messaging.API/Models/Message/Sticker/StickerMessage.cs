using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Sticker
{
    public class StickerMessage : BaseMessage
    {
        public string PackageId { get; }
        public string StickerId { get; }
        public string? QuoteToken { get; }

        public StickerMessage(
            string packageId,
            string stickerId,
            string? quoteToken = null,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Sticker, quickReply)
        {
            PackageId = packageId;
            StickerId = stickerId;
            QuoteToken = quoteToken;
        }
    }
}
