using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class ImageHelper
    {
        public static int sWidth = 30;

        public static int sHeight = 30;

        public static int sOffset = 20;

        public static int sTopOffset = 50;

        public static int sLeftOffset = 20;

        public int Row { get; set; }

        public int Column { get; set; }

        public int Left
        {
            get => Column * (sWidth + sLeftOffset) + sOffset;
        }

        public int Top
        {
            get => Row * (sHeight + sTopOffset) + sOffset;
        }

        public int Width { get { return sWidth; } }

        public int Height { get { return sHeight; } }

        public string ImagePath { get; set; }

        public ImageHelper(int column, int row, string path)
        {
            Column = column;
            Row = row;
            ImagePath = path;
        }

    }
}
