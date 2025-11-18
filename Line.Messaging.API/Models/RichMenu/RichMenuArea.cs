using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Event.Message;
using Line.Messaging.API.Models.Message.Action;
using static System.Collections.Specialized.BitVector32;

namespace Line.Messaging.API.Models.RichMenu
{
    public class RichMenuArea
    {
        public RichMenuAreaBound Bounds { get; }
        public ActionAction Action { get; }
        public RichMenuArea(
            RichMenuAreaBound bounds,
            ActionAction action)
        {
            Bounds = bounds;
            Action = action;
        }

        internal static RichMenuArea Create(dynamic areas)
        {
            RichMenuAreaBound bound = RichMenuAreaBound.Create(areas.bounds);

            ActionAction action = ActionAction.Create(areas.action);

            return new RichMenuArea(bound, action);
        }
    }
}
