using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class ImageHelper
    {
        public static int Offset { get; } = 20;

        public static int TopOffset { get; } = 50;

        public static int LeftOffset { get; } = 20;

        public int Row { get; set; }

        public int Column { get; set; }

        public int Left
        {
            get => Column * (Width + LeftOffset) + Offset;
        }

        public int Top
        {
            get => Row * (Height + TopOffset) + Offset;
        }

        public static int Width { get; } = 30;

        public static int Height { get; } = 30;

        public string ImagePath { get; set; }

        public ImageHelper(int column, int row, string path)
        {
            Column = column;
            Row = row;
            ImagePath = path;
        }

    }
}
