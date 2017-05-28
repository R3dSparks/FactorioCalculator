using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class Line
    {
        public int Top1 { get; set; }

        public int Left1 { get; set; }

        public int Top2 { get; set; }

        public int Left2 { get; set; }


        public Line(int x1, int y1, int x2, int y2)
        {
            Top1 = y1;
            Left1 = x1;

            Top2 = y2;
            Left2 = x2;
        }
    }
}
