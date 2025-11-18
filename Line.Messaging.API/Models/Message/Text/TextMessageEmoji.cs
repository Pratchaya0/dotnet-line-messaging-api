using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Text
{
    public class TextMessageEmoji
    {
        public int? Index { get; }
        public string? ProductId { get; }
        public string? EmojiId { get; }

        public TextMessageEmoji(
            int? index = null,
            string? productId = null,
            string? emojiId = null)
        {
            Index = index;
            ProductId = productId;
            EmojiId = emojiId;
        }

        internal static TextMessageEmoji Create(dynamic emoji)
        {
            return new TextMessageEmoji(
                index: (int?)emoji.index,
                productId: (string?)emoji.productId,
                emojiId: (string?)emoji.emojiId);
        }
    }
}
