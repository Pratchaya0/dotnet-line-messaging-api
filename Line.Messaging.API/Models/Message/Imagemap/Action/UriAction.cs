using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap.Action
{
    public class UriAction : ImagemapMessageActions
    {

        public string LinkUri { get; }
        public string? Label { get; }

        public UriAction(
            ActionArea area,
            string linkUri,
            string? label = null)
            : base(ActionType.Uri, area)
        {
            LinkUri = linkUri;
            Label = label;
        }
    }
}
