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

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get { return sWidth; } }

        public int Height { get { return sHeight; } }

        public string ImagePath { get; set; }

        public ImageHelper(int left, int top, string path)
        {
            Left = left;
            Top = top;
            ImagePath = path;
        }

    }
}
