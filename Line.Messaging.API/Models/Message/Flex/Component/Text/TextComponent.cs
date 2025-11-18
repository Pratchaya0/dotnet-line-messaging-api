using System.Collections.Generic;
using Line.Messaging.API.Models.Message.Action;
using Line.Messaging.API.Models.Message.Flex.Component.Span;

namespace Line.Messaging.API.Models.Message.Flex.Component.Text
{
    public class TextComponent : ComponentAndContent
    {
        public string? Text { get; }
        public IEnumerable<SpanComponent>? Contents { get; }
        public string? AdjustMode { get; }
        public string? Flex { get; }
        public string? Margin { get; }
        public PositionType? Position { get; }
        public string? OffsetTop { get; }
        public string? OffsetBottom { get; }
        public string? OffsetStart { get; }
        public string? OffsetEnd { get; }
        public string? Size { get; }
        public bool? Scaling { get; }
        public bool? Align { get; }
        public string? Gravity { get; }
        public bool? Wrap { get; }
        public string? LineSpacing { get; }
        public int? MaxLines { get; }
        public string? Weight { get; }
        public string? Color { get; }
        public ActionAction? Action { get; }
        public StyleType? Style { get; }
        public string? Decoration { get; }

        public TextComponent(
            string? text = null,
            IEnumerable<SpanComponent>? contents = null,
            string? adjustMode = null,
            string? flex = null,
            string? margin = null,
            PositionType? position = null,
            string? offsetTop = null,
            string? offsetBottom = null,
            string? offsetStart = null,
            string? offsetEnd = null,
            string? size = null,
            bool? scaling = null,
            bool? align = null,
            string? gravity = null,
            bool? wrap = null,
            string? lineSpacing = null,
            int? maxLines = null,
            string? weight = null,
            string? color = null,
            ActionAction? action = null,
            StyleType? style = null,
            string? decoration = null)
            : base(ComponentType.Text)
        {
            Text = text;
            Contents = contents;
            AdjustMode = adjustMode;
            Flex = flex;
            Margin = margin;
            Position = position;
            OffsetTop = offsetTop;
            OffsetBottom = offsetBottom;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
            Size = size;
            Scaling = scaling;
            Align = align;
            Gravity = gravity;
            Wrap = wrap;
            LineSpacing = lineSpacing;
            MaxLines = maxLines;
            Weight = weight;
            Color = color;
            Action = action;
            Style = style;
            Decoration = decoration;
        }
    }
}
