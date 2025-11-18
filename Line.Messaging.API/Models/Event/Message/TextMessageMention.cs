using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class TextMessageMention
    {
        public List<TextMessageMentionMentionee> Mentionees { get; }

        public TextMessageMention(List<TextMessageMentionMentionee> mentionees)
        {
            Mentionees = mentionees;
        }

        internal static TextMessageMention Create(dynamic mention)
        {
            var mentionees = new List<TextMessageMentionMentionee>();
            foreach (var mentionee in mention.mentionees)
            {
                mentionees.Add(TextMessageMentionMentionee.Create(mentionee));
            }
            return new TextMessageMention(mentionees);
        }
    }
}
