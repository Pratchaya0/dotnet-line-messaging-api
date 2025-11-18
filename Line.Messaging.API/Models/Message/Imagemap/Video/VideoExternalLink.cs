using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap.Video
{
    public class VideoExternalLink
    {
        public string LinkUri { get; }
        public string Label { get; }
        public VideoExternalLink(string linkUri, string label)
        {
            LinkUri = linkUri;
            Label = label;
        }

        internal static VideoExternalLink Create(dynamic externalLink)
        {
            return new VideoExternalLink((string)externalLink.linkUri, (string)externalLink.label);
        }
    }
}
