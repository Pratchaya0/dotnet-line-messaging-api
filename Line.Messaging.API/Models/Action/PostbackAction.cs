using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class PostbackAction : LabelableAction
    {
        public string Data { get; }
        public string? DisplayText { get; }
        public string FillInText { get; }
        public string? Text { get; }
        public string? InputOption { get; }

        public PostbackAction(
            string label,
            string data,
            string fillInText,
            string? displayText = null,
            string? text = null,
            string? inputOption = null) : base(label, ActionType.Postback)
        {
            Data = data;
            DisplayText = displayText;
            FillInText = fillInText;
            Text = text;
            InputOption = inputOption;
        }
    }
}
