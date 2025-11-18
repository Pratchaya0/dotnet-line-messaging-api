using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Coupon
{
    public class CouponMessage : BaseMessage
    {
        public string CouponId { get; }
        public string? DeliveryTag { get; }
        public CouponMessage(
            string couponId,
            string? deliveryTag = null,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Coupon, quickReply)
        {
            CouponId = couponId;
            DeliveryTag = deliveryTag;
        }
    }
}
