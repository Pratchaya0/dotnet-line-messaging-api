
namespace Line.Messaging.API.Models.Message.Flex.Component.Icon
{
    public class IconComponent : ComponentAndContent
    {
        public string Url { get; }
        public string? Margin { get; }
        public PositionType? Position { get; }
        public string? OffsetTop { get; }
        public string? OffsetBottom { get; }
        public string? OffsetStart { get; }
        public string? OffsetEnd { get; }
        public string? Size { get; }
        public bool? Scaling { get; }
        public string? AspectRatio { get; }

        public IconComponent(
            string url,
            string? margin = null,
            PositionType? position = null,
            string? offsetTop = null,
            string? offsetBottom = null,
            string? offsetStart = null,
            string? offsetEnd = null,
            string? size = null,
            bool? scaling = null,
            string? aspectRatio = null)
            : base(ComponentType.Icon)
        {
            Url = url;
            Margin = margin;
            Position = position;
            OffsetTop = offsetTop;
            OffsetBottom = offsetBottom;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
            Size = size;
            Scaling = scaling;
            AspectRatio = aspectRatio;
        }
    }
}
