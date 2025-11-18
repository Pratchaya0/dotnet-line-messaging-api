using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class StickerMessage : MessageEventMessage
    {
        public string PackageId { get; }
        public string StickerId { get; }
        public string QuoteToken { get; }
        public string StickerResourceType { get; set; }
        public IEnumerable<string>? Keywords { get; }
        public string? Text { get; }
        public string? QuotedMessageId { get; }


        public StickerMessage(
            string id,
            string markAsReadToken,
            string packageId,
            string stickerId,
            string quoteToken,
            string stickerResourceType,
            IEnumerable<string>? keywords = null,
            string? text = null,
            string? quotedMessageId = null)
            : base(id, MessageType.Sticker, markAsReadToken)
        {
            PackageId = packageId;
            StickerId = stickerId;
            QuoteToken = quoteToken;
            StickerResourceType = stickerResourceType;
            Keywords = keywords;
            Text = text;
            QuotedMessageId = quotedMessageId;
        }
    }
}
