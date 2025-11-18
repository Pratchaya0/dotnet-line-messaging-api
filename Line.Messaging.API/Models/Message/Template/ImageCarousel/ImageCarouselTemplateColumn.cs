using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line.Messaging.API.Models.Message.Action;

namespace Line.Messaging.API.Models.Message.Template.ImageCarousel
{
    public class ImageCarouselTemplateColumn
    {
        public string ImageUrl { get; }
        public IEnumerable<ActionAction> Actions { get; }

        public ImageCarouselTemplateColumn(
            string imageUrl,
            IEnumerable<ActionAction> actions)
        {
            ImageUrl = imageUrl;
            Actions = actions;
        }

        internal static ImageCarouselTemplateColumn Create(dynamic column)
        {
            var actions = new List<ActionAction>();
            foreach (var action in column.actions)
            {
                actions.Add(ActionAction.Create(action));
            }

            return new ImageCarouselTemplateColumn(
                (string)column.imageUrl,
                actions);
        }
    }
}
