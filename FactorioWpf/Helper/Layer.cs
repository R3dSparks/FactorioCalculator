using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class Layer
    {
        private List<AssemblyImageHelper> m_assemblyImages;

        public List<AssemblyImageHelper> AssemblyImages
        {
            get
            {
                if (m_assemblyImages == null)
                    m_assemblyImages = new List<AssemblyImageHelper>();

                return m_assemblyImages;
            }
        }

        public int Depth { get; set; }

        public static int Height { get; private set; } = 40;

        public int Size { get; set; } = 0;

        /// <summary>
        /// Size of gap between two images
        /// </summary>
        public static int GapSize { get; set; } = 20;

        public Layer(int depth)
        {
            Depth = depth;
        }

        public void AddAssembly(FactorioAssembly assembly, int position)
        {
            AssemblyImages.Add(new AssemblyImageHelper(position, Depth * Height, assembly));
        }
    }
}
