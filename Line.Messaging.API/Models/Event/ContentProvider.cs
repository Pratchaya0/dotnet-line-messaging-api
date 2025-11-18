using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event
{
    public class ContentProvider
    {
        public ContentProviderType Type { get; }
        public string OriginalContentUrl { get; }
        public string PreviewImageUrl { get; }

        public ContentProvider(ContentProviderType type, string originalContentUrl, string previewImageUrl)
        {
            Type = type;
            OriginalContentUrl = originalContentUrl;
            PreviewImageUrl = previewImageUrl;
        }
    }
}
