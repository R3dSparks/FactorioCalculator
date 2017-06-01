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

        /// <summary>
        /// Depth of this layer in the tree structure
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// Height of the layer
        /// </summary>
        public static int Height { get; private set; } = 40;

        /// <summary>
        /// Width of this layer
        /// </summary>
        public int Width { get; set; } = 0;

        public Layer(int depth)
        {
            Depth = depth;
        }

        public void AddAssembly(FactorioAssembly assembly, int position)
        {
            if(position >= Width)
            {
                AssemblyImages.Add(new AssemblyImageHelper(position, Depth * Height, assembly));

                Width += (position - Width) + AssemblyImageHelper.Width;
            }
            else
            {
                AssemblyImages.Add(new AssemblyImageHelper(Width, Depth * Height, assembly));

                Width += AssemblyImageHelper.Width;
            }

        }
    }
}
