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

        private int m_width = 0;

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
        /// Width of this layer
        /// </summary>
        public int Width {
            get
            {
                return m_width;
            }
            set
            {
                m_width = value;
                if (m_width > MaxWidth)
                    MaxWidth = m_width;
            }
        }

        public int MaxWidth { get; set; } = 0;

        public Layer(int depth)
        {
            Depth = depth;
        }

        public AssemblyImageHelper AddAssembly(FactorioAssembly assembly, int position)
        {
            AssemblyImageHelper image;

            if(position >= Width)
            {
                image = new AssemblyImageHelper(position, Depth * AssemblyImageHelper.Height, assembly);

                Width += (position - Width) + AssemblyImageHelper.Width;
            }
            else
            {
                image = new AssemblyImageHelper(Width, Depth * AssemblyImageHelper.Height, assembly);

                Width += AssemblyImageHelper.Width;
            }

            AssemblyImages.Add(image);

            return image;

        }
    }
}
