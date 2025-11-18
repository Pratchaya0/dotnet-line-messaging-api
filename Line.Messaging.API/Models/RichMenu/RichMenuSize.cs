using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.RichMenu
{
    public class RichMenuSize
    {
        public int Width { get; }
        public int Height { get; }
        public RichMenuSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        internal static RichMenuSize Create(dynamic size)
        {
            return new RichMenuSize(
                (int)size.width,
                (int)size.height);
        }
    }
}
