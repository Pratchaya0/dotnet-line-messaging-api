using System;

namespace Line.Messaging.API.Models.Message.Flex
{
    public class FlexMessage : BaseMessage
    {
        public string AltText { get; }
        public FlexMessageContents Contents { get; }

        public FlexMessage(
            string altText,
            FlexMessageContents contents,
            MessageQuickReply? quickReply = null)
            : base(MessageType.Flex, quickReply)
        {
            AltText = altText;
            Contents = contents;
        }
    }
}
