using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class UriAction : LabelableAction
    {
        public string Uri { get; }

        public UriAction(
            string label,
            string uri) : base(label, ActionType.Uri)
        {
            Uri = uri;
        }
    }
}
