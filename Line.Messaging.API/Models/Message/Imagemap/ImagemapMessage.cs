using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap
{
    public class ImagemapMessage : BaseMessage
    {
        public string BaseUrl { get; }
        public string AltText { get; }
        public ImagemapMessageBaseSize BaseSize { get; }
        public IEnumerable<ImagemapMessageActions> Actions { get; }
        public ImagemapMessageVideo? Video { get; }

        public ImagemapMessage(

            MessageQuickReply? quickReply = null)
            : base(MessageType.Imagemap, quickReply)
        {
        }
    }
}
