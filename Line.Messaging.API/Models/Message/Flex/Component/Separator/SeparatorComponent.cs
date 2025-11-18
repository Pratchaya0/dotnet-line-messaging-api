
namespace Line.Messaging.API.Models.Message.Flex.Component.Separator
{
    public class SeparatorComponent : ComponentAndContent
    {
        public string? Margin { get; }
        public string? Color { get; }
        public SeparatorComponent(
            string? margin = null,
            string? color = null)
            : base(ComponentType.Separator)
        {
            Margin = margin;
            Color = color;
        }
    }
}
