using System;

namespace Line.Messaging.API.Models.Message.Flex.Component
{
    public abstract class ComponentAndContent
    {
        public ComponentType Type { get; }

        protected ComponentAndContent(ComponentType type)
        {
            Type = type;
        }

        internal static ComponentAndContent Create(dynamic component)
        {
            if (!Enum.TryParse((string)component.type, true, out ComponentType type))
            {
                throw new InvalidCastException($"Component type '{component.type}' is invalid.");
            }

            throw new NotImplementedException();
        }
    }
}
