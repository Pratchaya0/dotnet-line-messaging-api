using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap.Video
{
    public class VideoArea
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public VideoArea(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        internal static VideoArea Create(dynamic area)
        {
            return new VideoArea(
                (int)area.x,
                (int)area.y,
                (int)area.width,
                (int)area.height);
        }
    }
}
