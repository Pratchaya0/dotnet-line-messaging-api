using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Line.Messaging.API.Models.RichMenu;

namespace Line.Messaging.API.Models.Response
{
    public class RichMenuResponse
    {
        [JsonPropertyName("richmenus")]
        public IEnumerable<BaseRichMenu> RichMenus { get; set; }
    }
}
