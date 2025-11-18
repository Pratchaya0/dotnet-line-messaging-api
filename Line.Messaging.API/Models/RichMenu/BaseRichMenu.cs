using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.RichMenu
{
    public abstract class BaseRichMenu
    {
        public RichMenuSize Size { get; }
        public bool Selected { get; }
        public string Name { get; }
        public string ChatBarText { get; }
        public IList<RichMenuArea> Areas { get; }

        public BaseRichMenu(
            RichMenuSize size,
            bool selected,
            string name,
            string chatBarText,
            IList<RichMenuArea> areas)
        {
            Size = size;
            Selected = selected;
            Name = name;
            ChatBarText = chatBarText;
            Areas = areas;
        }
    }
}
