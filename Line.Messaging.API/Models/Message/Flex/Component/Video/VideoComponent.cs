using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Flex.Component.Video
{
    public class VideoComponent : ComponentAndContent
    {
        public string Url { get; }
        public string PreviewImageUrl { get; }
        public string AltContent { get; }
        public string? AspectRatio { get; }
        public ActionAction? Action { get; }
        public VideoComponent(
            string url,
            string previewImageUrl,
            string altContent,
            string? aspectRatio = null,
            ActionAction? action = null)
            : base(ComponentType.Video)
        {
            Url = url;
            PreviewImageUrl = previewImageUrl;
            AltContent = altContent;
            AspectRatio = aspectRatio;
            Action = action;
        }
    }
}
