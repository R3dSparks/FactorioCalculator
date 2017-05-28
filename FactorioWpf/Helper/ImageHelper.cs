using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class ImageHelper
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public string ImagePath { get; set; }

        public ImageHelper(int left, int top, string path)
        {
            Left = left;
            Top = top;
            ImagePath = path;
        }

    }
}
