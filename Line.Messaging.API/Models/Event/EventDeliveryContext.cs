using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event
{
    public class EventDeliveryContext
    {
        public bool IsRedelivery { get; }

        public EventDeliveryContext(bool isRedelivery)
        {
            IsRedelivery = isRedelivery;
        }

        internal static EventDeliveryContext Create(dynamic deliveryContext)
        {
            bool isRedelivery = (bool)deliveryContext.isRedelivery;
            return new EventDeliveryContext(isRedelivery);
        }
    }
}
