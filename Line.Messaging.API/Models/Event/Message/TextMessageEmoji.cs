using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class TextMessageEmoji
    {
        public int Index { get; }
        public int Length { get; }
        public string ProductId { get; }
        public string EmojiId { get; }

        public TextMessageEmoji(int index, int length, string productId, string emojiId)
        {
            Index = index;
            Length = length;
            ProductId = productId;
            EmojiId = emojiId;
        }

        internal static TextMessageEmoji Create(dynamic emoji)
        {
            return new TextMessageEmoji(
                (int)emoji.index,
                (int)emoji.length,
                (string)emoji.productId,
                (string)emoji.emojiId);
        }
    }
}
