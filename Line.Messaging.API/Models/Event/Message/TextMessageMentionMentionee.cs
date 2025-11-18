using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class TextMessageMentionMentionee
    {
        public int Index { get; }
        public int Length { get; }
        public TextMessageMentionMentioneeType Type { get; }
        public string UserId { get; }
        public bool IsSelf { get; }

        public TextMessageMentionMentionee(
            int index,
            int length,
            TextMessageMentionMentioneeType type,
            string userId,
            bool isSelf)
        {
            Index = index;
            Length = length;
            Type = type;
            UserId = userId;
            IsSelf = isSelf;
        }

        internal static TextMessageMentionMentionee Create(dynamic mentionee)
        {
            int index = (int)mentionee.index;
            int length = (int)mentionee.length;
            if (!Enum.TryParse((string)mentionee.type, true, out TextMessageMentionMentioneeType type))
            {
                type = TextMessageMentionMentioneeType.User;
            }
            string userId = (string)mentionee.userId;
            bool isSelf = (bool)mentionee.isSelf;
            return new TextMessageMentionMentionee(
                index,
                length,
                type,
                userId,
                isSelf);
        }
    }
}
