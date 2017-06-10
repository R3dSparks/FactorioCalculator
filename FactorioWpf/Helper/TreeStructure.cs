using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class TreeStructure
    {
        private List<Layer> m_layers;

        private List<Line> m_lines;

        public List<Line> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<Line>();

                return m_lines;
            }
        }


        public TreeStructure(FactorioAssembly assembly)
        {
            m_layers = new List<Layer>(1);

            createLayer(assembly, 0, 0);
        }

        private AssemblyImageHelper createLayer(FactorioAssembly assembly, int layer, int position)
        {
            if (m_layers.Count <= layer)
                m_layers.Add(new Layer(layer));

            AssemblyImageHelper image = m_layers[layer].AddAssembly(assembly, position);

            foreach (var subAssembly in assembly.SubAssembly)
            {

                int nextPosition = position;

                if (m_layers.Count > layer + 1 && m_layers[layer + 1].MaxWidth > position)
                {
                    nextPosition = m_layers[layer + 1].MaxWidth;
                }

                AssemblyImageHelper subImage = createLayer(subAssembly, layer + 1, nextPosition);

                Lines.Add(new Line(image, subImage));

                if(m_layers[layer].MaxWidth < m_layers[layer + 1].MaxWidth)
                    m_layers[layer].MaxWidth = m_layers[layer + 1].MaxWidth;
            }

            return image;
        }

        internal List<AssemblyImageHelper> GetImageList()
        {
            List<AssemblyImageHelper> images = new List<AssemblyImageHelper>();

            foreach (var layer in m_layers)
            {
                images.AddRange(layer.AssemblyImages);
            }

            return images;
        }
    }
}
