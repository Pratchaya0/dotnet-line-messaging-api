using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.RichMenu
{
    public class RichMenuAreaBound
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public RichMenuAreaBound(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        internal static RichMenuAreaBound Create(dynamic bound)
        {
            return new RichMenuAreaBound(
                (int)bound.x,
                (int)bound.y,
                (int)bound.width,
                (int)bound.height);
        }
    }
}
