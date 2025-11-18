using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public abstract class MessageEventMessage
    {
        public string Id { get; }
        public MessageType Type { get; }
        public string MarkAsReadToken { get; }

        public MessageEventMessage(string id, MessageType type, string markAsReadToken)
        {
            Id = id;
            Type = type;
            MarkAsReadToken = markAsReadToken;
        }

        internal static MessageEventMessage Create(dynamic message)
        {
            string id = (string)message.id;
            string markAsReadToken = (string)message.markAsReadToken;
            if (!Enum.TryParse((string)message.type, true, out MessageType type))
            {
                type = MessageType.Text;
            }

            switch (type)
            {
                case MessageType.Text:
                    return new TextMessage(
                        id, markAsReadToken, (string)message.quoteToken, (string)message.text);
                case MessageType.Image:
                case MessageType.Video:
                    ContentProvider? contentProvider = null;
                    if (Enum.TryParse((string)message.contentProvider.type, true, out ContentProviderType providerType))
                    {
                        contentProvider = new ContentProvider(
                                providerType,
                                message.contentProvider?.originalContentUrl,
                                message.contentProvider?.previewContentUrl
                            );
                    }
                    return new MediaMessage(
                        id, type, markAsReadToken, contentProvider, (int)message.duration, (string)message.quoteToken);
                case MessageType.Audio:
                    ContentProvider? audioContentProvider = null;
                    if (Enum.TryParse((string)message.contentProvider.type, true, out ContentProviderType audioProviderType))
                    {
                        audioContentProvider = new ContentProvider(
                                audioProviderType,
                                message.contentProvider?.originalContentUrl,
                                message.contentProvider?.previewContentUrl
                            );
                    }
                    return new AudioMessage(
                        id, markAsReadToken, audioContentProvider, (int)message.duration);
                case MessageType.File:
                    return new FileMessage(
                        id, type, markAsReadToken, (string)message.fileName, (long)message.fileSize);
                case MessageType.Location:
                    return new LocationMessage(
                            id,
                            markAsReadToken,
                            (long)message.latitude,
                            (long)message.longitude,
                            (string?)message.title,
                            (string?)message.address
                        );
                case MessageType.Sticker:
                    var keywords = new List<string>();
                    if (message.keywords! != null)
                    {
                        foreach (var kw in message.keywords)
                        {
                            keywords.Add((string)kw);
                        }
                    }

                    return new StickerMessage(
                        id, markAsReadToken,
                        (string)message.packageId,
                        (string)message.stickerId,
                        (string)message.queteToken,
                        (string)message.stickerResourceType,
                        keywords,
                        (string?)message?.text,
                        (string?)message?.quotedMessageId);
                default:
                    throw new NotSupportedException($"Message type '{type}' is not supported.");
            }
        }
    }
}
