using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.RichMenu
{
    /// <summary>
    /// Rich menu operation object represents the batch operation to the rich menu linked to the user.
    /// </summary>
    public class RichMenuOperation
    {
        public RichMenuOperationType Type { get; }

        public string? From { get; } = null;
        public string? To { get; } = null;
    }
}
