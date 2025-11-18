using System;

namespace Line.Messaging.API.Models.Message.Flex
{
    public abstract class FlexMessageContents
    {
        public FlexMessageContentType Type { get; }
        public FlexMessageContents(FlexMessageContentType type)
        {
            Type = type;
        }

        internal static FlexMessageContents Create(dynamic contents)
        {
            if (!Enum.TryParse((string)contents.type, true, out FlexMessageContentType type))
            {
                throw new InvalidCastException($"Flex contents type '{contents.type}' is invalid.");
            }

            throw new NotImplementedException();
        }
    }
}
