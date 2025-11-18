using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class RichmenuSwitchAction : ActionAction
    {
        public string? Label { get; }
        public string RichMenuAliasId { get; }
        public string Data { get; }
        public RichmenuSwitchAction(
            string richMenuAliasId,
            string data,
            string? label = null) : base(ActionType.RichmenuSwitch)
        {
            Label = label;
            RichMenuAliasId = richMenuAliasId;
            Data = data;
        }
    }
}
