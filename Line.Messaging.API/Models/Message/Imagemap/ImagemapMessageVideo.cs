using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Imagemap.Video;

namespace Line.Messaging.API.Models.Message.Imagemap
{
    public class ImagemapMessageVideo
    {
        public string OriginalContentUrl { get; }
        public string PreviewImageUrl { get; }
        public VideoArea Area { get; }
        public VideoExternalLink ExternalLink { get; }
        public ImagemapMessageVideo(
            string originalContentUrl,
            string previewImageUrl,
            VideoArea area,
            VideoExternalLink externalLink)
        {
            OriginalContentUrl = originalContentUrl;
            PreviewImageUrl = previewImageUrl;
            Area = area;
            ExternalLink = externalLink;
        }

        internal static ImagemapMessageVideo Create(dynamic video)
        {
            VideoArea area = VideoArea.Create(video.area);
            VideoExternalLink externalLink = VideoExternalLink.Create(video.externalLink);
            return new ImagemapMessageVideo(
                (string)video.originalContentUrl,
                (string)video.previewImageUrl,
                area,
                externalLink);
        }
    }
}
