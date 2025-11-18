using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Template.Buttons
{
    public class ButtonsTemplate : TemplateMessageTemplate
    {
        public string Text { get; }
        public IEnumerable<ActionAction> Actions { get; }
        public string? ThumbnailImageUrl { get; }
        public string? ImageAspectRatio { get; }
        public string? ImageSize { get; }
        public string? ImageBackgroundColor { get; }
        public string? Title { get; }
        public ActionAction? DefaultAction { get; }

        public ButtonsTemplate(
            string text,
            IEnumerable<ActionAction> actions,
            string? thumbnailImageUrl = null,
            string? imageAspectRatio = null,
            string? imageSize = null,
            string? imageBackgroundColor = null,
            string? title = null,
            ActionAction? defaultAction = null)
            : base(TemplateMessageTemplateType.Buttons)
        {
            Text = text;
            Actions = actions;
            ThumbnailImageUrl = thumbnailImageUrl;
            ImageAspectRatio = imageAspectRatio;
            ImageSize = imageSize;
            ImageBackgroundColor = imageBackgroundColor;
            Title = title;
            DefaultAction = defaultAction;
        }
    }
}
