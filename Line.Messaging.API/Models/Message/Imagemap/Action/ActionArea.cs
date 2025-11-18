using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Message.Imagemap.Action
{
    public class ActionArea
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public ActionArea(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        internal static ActionArea Create(dynamic area)
        {
            return new ActionArea(
                (int)area.x,
                (int)area.y,
                (int)area.width,
                (int)area.height);
        }
    }
}
