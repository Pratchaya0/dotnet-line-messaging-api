using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Action;

namespace Line.Messaging.API.Models.Message.Action
{
    public class DatetimePickerAction : LabelableAction
    {
        public string Data { get; }
        public DatetimePickerActionMode Mode { get; }
        public string? Initial { get; }
        public string? Max { get; }
        public string? Min { get; }

        public DatetimePickerAction(
            string label,
            string data,
            DatetimePickerActionMode mode,
            string? initial = null,
            string? max = null,
            string? min = null) : base(label, ActionType.DatetimePicker)
        {
            Data = data;
            Mode = mode;
            Initial = initial;
            Max = max;
            Min = min;
        }
    }
}
