using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Flex.Component.Button
{
    public class ButtonComponent : ComponentAndContent
    {
        public ActionAction Action { get; }
        public int? Flex { get; }
        public string? Margin { get; }
        public PositionType? Position { get; }
        public string? OffsetTop { get; }
        public string? OffsetBottom { get; }
        public string? OffsetStart { get; }
        public string? OffsetEnd { get; }
        public string? Height { get; }
        public string? Style { get; }
        public string? Color { get; }
        public string? Gravity { get; }
        public string? AdjustMode { get; }
        public bool? Scaling { get; }

        public ButtonComponent(
            ActionAction action,
            int? flex = null,
            string? margin = null,
            PositionType? position = null,
            string? offsetTop = null,
            string? offsetBottom = null,
            string? offsetStart = null,
            string? offsetEnd = null,
            string? height = null,
            string? style = null,
            string? color = null,
            string? gravity = null,
            string? adjustMode = null,
            bool? scaling = null)
            : base(ComponentType.Button)
        {
            Action = action;
            Flex = flex;
            Margin = margin;
            Position = position;
            OffsetTop = offsetTop;
            OffsetBottom = offsetBottom;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
            Height = height;
            Style = style;
            Color = color;
            Gravity = gravity;
            AdjustMode = adjustMode;
            Scaling = scaling;
        }
    }
}
