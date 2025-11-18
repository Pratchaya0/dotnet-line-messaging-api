using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.TextV2.Substitution;

namespace Line.Messaging.API.Models.Message.TextV2
{
    public class TextV2MessageSubstitution
    {
        public TextV2MessageSubstitutionType Type { get; }
        public string? ProductId { get; }
        public string? EmojiId { get; }
        public SubstitutionMentionee? Mentionee { get; }

        public TextV2MessageSubstitution(
            TextV2MessageSubstitutionType type,
            string? productId = null,
            string? emojiId = null,
            SubstitutionMentionee? mentionee = null)
        {
            Type = type;
            ProductId = productId;
            EmojiId = emojiId;
            Mentionee = mentionee;
        }

        internal static TextV2MessageSubstitution Create(dynamic substitution)
        {
            if (!Enum.TryParse((string)substitution.type, true, out TextV2MessageSubstitutionType type))
            {
                type = TextV2MessageSubstitutionType.Emoji;
            }
            string? productId = null;
            string? emojiId = null;
            SubstitutionMentionee? mentionee = null;
            switch (type)
            {
                case TextV2MessageSubstitutionType.Emoji:
                    productId = (string)substitution.productId;
                    emojiId = (string)substitution.emojiId;
                    break;
                case TextV2MessageSubstitutionType.Mention:
                    mentionee = SubstitutionMentionee.Create(substitution.mentionee);
                    break;
            }
            return new TextV2MessageSubstitution(
                type,
                productId,
                emojiId,
                mentionee);
        }
    }
}
