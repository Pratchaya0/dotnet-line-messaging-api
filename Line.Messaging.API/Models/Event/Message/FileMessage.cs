using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Message
{
    public class FileMessage : MessageEventMessage
    {
        public string FileName { get; }
        public long FileSize { get; }

        public FileMessage(
                string id,
                MessageType type,
                string markAsReadToken,
                string fileName,
                long fileSize)
            : base(id, type, markAsReadToken)
        {
            FileName = fileName;
            FileSize = fileSize;
        }
    }
}
