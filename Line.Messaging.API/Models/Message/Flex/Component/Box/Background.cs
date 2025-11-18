
namespace Line.Messaging.API.Models.Message.Flex.Component.Box
{
    public class Background
    {
        public string? Type { get; }
        public string? Angle { get; }
        public string? StartColor { get; }
        public string? EndColor { get; }
        public string? CenterColor { get; }
        public string? CenterPosition { get; }

        public Background(
            string? type = null,
            string? angle = null,
            string? startColor = null,
            string? endColor = null,
            string? centerColor = null,
            string? centerPosition = null)
        {
            Type = type;
            Angle = angle;
            StartColor = startColor;
            EndColor = endColor;
            CenterColor = centerColor;
            CenterPosition = centerPosition;
        }

        internal static Background Create(dynamic background)
        {
            return new Background(
                (string?)background?.type,
                (string?)background?.angle,
                (string?)background?.startColor,
                (string?)background?.endColor,
                (string?)background?.centerColor,
                (string?)background?.centerPosition);
        }
    }
}
