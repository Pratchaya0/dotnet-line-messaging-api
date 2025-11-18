using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class CameraAction : LabelableAction
    {
        public CameraAction(string label) : base(label, ActionType.Camera)
        {
        }
    }
}
