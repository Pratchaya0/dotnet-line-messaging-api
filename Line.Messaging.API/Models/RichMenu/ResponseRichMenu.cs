using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.RichMenu
{
    public abstract class ResponseRichmenu : BaseRichMenu
    {
        public string RichMenuId { get; }
        protected ResponseRichmenu(
            RichMenuSize size,
            bool selected,
            string name,
            string chatBarText,
            IList<RichMenuArea> areas)
            : base(size, selected, name, chatBarText, areas)
        {
        }
    }
}
