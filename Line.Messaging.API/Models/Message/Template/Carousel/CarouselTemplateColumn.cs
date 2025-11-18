using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Template.Carousel
{
    public class CarouselTemplateColumn
    {
        public string Text { get; }
        public IEnumerable<ActionAction> Actions { get; }
        public string? Title { get; }
        public string? ThumbnailImageUrl { get; }
        public string? ImageBackgroundColor { get; }
        public ActionAction? DefaultAction { get; }

        public CarouselTemplateColumn(
            string text,
            IEnumerable<ActionAction> actions,
            string? title = null,
            string? thumbnailImageUrl = null,
            string? imageBackgroundColor = null,
            ActionAction? defaultAction = null)
        {
            Title = title;
            Text = text;
            ThumbnailImageUrl = thumbnailImageUrl;
            ImageBackgroundColor = imageBackgroundColor;
            DefaultAction = defaultAction;
            Actions = actions;
        }

        internal static CarouselTemplateColumn Create(dynamic column)
        {
            var actions = new List<ActionAction>();
            foreach (var action in column.actions)
            {
                actions.Add(ActionAction.Create(action));
            }

            ActionAction? defaultAction = null;
            if (column.defaultAction != null)
            {
                defaultAction = ActionAction.Create(column.defaultAction);
            }

            return new CarouselTemplateColumn(
                (string)column.text,
                actions,
                (string?)column.title,
                (string?)column.thumbnailImageUrl,
                (string?)column.imageBackgroundColor,
                defaultAction
            );
        }
    }
}
