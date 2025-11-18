using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap
{
    public class ImagemapMessageBaseSize
    {
        public int Width { get; }
        public int Height { get; }

        public ImagemapMessageBaseSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        internal static ImagemapMessageBaseSize Create(dynamic baseSize)
        {
            return new ImagemapMessageBaseSize((int)baseSize.width, (int)baseSize.height);
        }
    }
}
