
namespace Line.Messaging.API.Models.Message.Flex.Content
{
    public class BubbleContent : FlexMessageContents
    {

        public ContentSizeType? Size { get; }
        public ContentDirectionType? Direction { get; }

        public BubbleContent(
            ContentSizeType? size = null,
            ContentDirectionType? direction = null)
            : base(FlexMessageContentType.Bubble)
        {
            Size = size;
            Direction = direction;
        }
    }
}
