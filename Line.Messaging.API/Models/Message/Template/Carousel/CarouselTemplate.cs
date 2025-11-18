using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Template.Carousel
{
    public class CarouselTemplate : TemplateMessageTemplate
    {
        public IEnumerable<CarouselTemplateColumn> Columns { get; }
        public string? ImageAspectRatio { get; }
        public string? ImageSize { get; }

        public CarouselTemplate(
            IEnumerable<CarouselTemplateColumn> columns,
            string? imageAspectRatio = null,
            string? imageSize = null)
            : base(TemplateMessageTemplateType.Carousel)
        {
            Columns = columns;
            ImageAspectRatio = imageAspectRatio;
            ImageSize = imageSize;
        }
    }
}
