using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.TextV2.Substitution
{
    public class SubstitutionMentionee
    {
        public SubstitutionMentioneeType Type { get; }
        public string? UserId { get; }

        public SubstitutionMentionee(SubstitutionMentioneeType type, string? userId = null)
        {
            Type = type;
            UserId = userId;
        }

        internal static SubstitutionMentionee Create(dynamic mentionee)
        {
            if (!Enum.TryParse((string)mentionee.type, true, out SubstitutionMentioneeType type))
            {
                type = SubstitutionMentioneeType.User;
            }

            string? userId = null;

            if (mentionee.userId != null)
            {
                userId = (string)mentionee.userId;
            }

            return new SubstitutionMentionee(type, userId);
        }
    }
}
