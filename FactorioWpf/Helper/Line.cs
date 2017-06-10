using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class Line
    {
        private AssemblyImageHelper m_start;

        private AssemblyImageHelper m_end;

        public int Top1 { get => m_start.Top + AssemblyImageHelper.ImageHeight; }

        public int Left1 { get => m_start.Left + AssemblyImageHelper.ImageWidth / 2; }

        public int Top2 { get => m_end.Top; }

        public int Left2 { get => m_end.Left + AssemblyImageHelper.ImageWidth / 2; }

        public Line(AssemblyImageHelper start, AssemblyImageHelper end)
        {
            m_start = start;
            m_end = end;
        }
    }
}
