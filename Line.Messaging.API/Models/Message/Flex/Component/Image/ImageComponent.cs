using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Flex.Component.Image
{
    public class ImageComponent : ComponentAndContent
    {
        public string Url { get; }
        public int? Flex { get; }
        public string? Margin { get; }
        public PositionType? Position { get; }
        public string? OffsetTop { get; }
        public string? OffsetBottom { get; }
        public string? OffsetStart { get; }
        public string? OffsetEnd { get; }
        public string? Align { get; }
        public string? Gravity { get; }
        public string? Size { get; }
        public string? AspectRatio { get; }
        public string? AspectMode { get; }
        public string? BackgroundColor { get; }
        public ActionAction? Action { get; }
        public bool? Animated { get; }
        public ImageComponent(
            string url,
            int? flex = null,
            string? margin = null,
            PositionType? position = null,
            string? offsetTop = null,
            string? offsetBottom = null,
            string? offsetStart = null,
            string? offsetEnd = null,
            string? align = null,
            string? gravity = null,
            string? size = null,
            string? aspectRatio = null,
            string? aspectMode = null,
            string? backgroundColor = null,
            ActionAction? action = null,
            bool? animated = null) : base(ComponentType.Image)
        {
            Url = url;
            Flex = flex;
            Margin = margin;
            Position = position;
            OffsetTop = offsetTop;
            OffsetBottom = offsetBottom;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
            Align = align;
            Gravity = gravity;
            Size = size;
            AspectRatio = aspectRatio;
            AspectMode = aspectMode;
            BackgroundColor = backgroundColor;
            Action = action;
            Animated = animated;
        }
    }
}
