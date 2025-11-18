
namespace Line.Messaging.API.Models.Message.Flex.Component.Span
{
    public class SpanComponent : ComponentAndContent
    {
        public string? Text { get; }
        public string? Color { get; }
        public string? Size { get; }
        public string? Weight { get; }
        public string? Style { get; }
        public string? Decoration { get; }
        public SpanComponent(
            string? text = null,
            string? color = null,
            string? size = null,
            string? weight = null,
            string? style = null,
            string? decoration = null) : base(ComponentType.Span)
        {
            Text = text;
            Color = color;
            Size = size;
            Weight = weight;
            Style = style;
            Decoration = decoration;
        }

        internal static new SpanComponent Create(dynamic component)
        {
            return new SpanComponent(
                (string?)component.text,
                (string?)component.color,
                (string?)component.size,
                (string?)component.weight,
                (string?)component.style,
                (string?)component.decoration
                );
        }
    }
}
