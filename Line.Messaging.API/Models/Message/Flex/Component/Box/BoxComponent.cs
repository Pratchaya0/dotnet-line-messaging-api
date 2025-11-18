using System.Collections.Generic;
using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Flex.Component.Box
{
    public class BoxComponent : ComponentAndContent
    {
        public OrientationType Layout { get; }
        public IEnumerable<ComponentAndContent> Contents { get; }
        public string? BackgroundColor { get; }
        public string? BorderColor { get; }
        public string? BorderWidth { get; }
        public string? CornerRadius { get; }
        public string? Width { get; }
        public string? MaxWidth { get; }
        public string? Height { get; }
        public string? MaxHeight { get; }
        public int? Flex { get; }
        public string? Spacing { get; }
        public string? Margin { get; }
        public string? PaddingAll { get; }
        public string? PaddingTop { get; }
        public string? PaddingBottom { get; }
        public string? PaddingStart { get; }
        public string? PaddingEnd { get; }
        public PositionType? Position { get; }
        public string? OffsetTop { get; }
        public string? OffsetBottom { get; }
        public string? OffsetStart { get; }
        public string? OffsetEnd { get; }
        public ActionAction? Action { get; }
        public string? JustifyContent { get; }
        public string? AlignItems { get; }
        public Background? Background { get; }

        public BoxComponent(
            OrientationType layout,
            IEnumerable<ComponentAndContent> contents,
            string? backgroundColor = null,
            string? borderColor = null,
            string? borderWidth = null,
            string? cornerRadius = null,
            string? width = null,
            string? maxWidth = null,
            string? height = null,
            string? maxHeight = null,
            int? flex = null,
            string? spacing = null,
            string? margin = null,
            string? paddingAll = null,
            string? paddingTop = null,
            string? paddingBottom = null,
            string? paddingStart = null,
            string? paddingEnd = null,
            PositionType? position = null,
            string? offsetTop = null,
            string? offsetBottom = null,
            string? offsetStart = null,
            string? offsetEnd = null,
            ActionAction? action = null,
            string? justifyContent = null,
            string? alignItems = null,
            Background? background = null)
            : base(ComponentType.Box)
        {
            Layout = layout;
            Contents = contents;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
            BorderWidth = borderWidth;
            CornerRadius = cornerRadius;
            Width = width;
            MaxWidth = maxWidth;
            Height = height;
            MaxHeight = maxHeight;
            Flex = flex;
            Spacing = spacing;
            Margin = margin;
            PaddingAll = paddingAll;
            PaddingTop = paddingTop;
            PaddingBottom = paddingBottom;
            PaddingStart = paddingStart;
            PaddingEnd = paddingEnd;
            Position = position;
            OffsetTop = offsetTop;
            OffsetBottom = offsetBottom;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
            Action = action;
            JustifyContent = justifyContent;
            AlignItems = alignItems;
            Background = background;
        }
    }
}
