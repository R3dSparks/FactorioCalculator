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

        public TreeStructure(FactorioAssembly assembly)
        {
            m_layers = new List<Layer>();

            createStructure(assembly, 0, 0);
        }

        private void createStructure(FactorioAssembly assembly, int layer, int position)
        {
            if (m_layers[layer] == null)
                m_layers[layer] = new Layer(layer);

            m_layers[layer].AddAssembly(assembly, position);

            foreach (var subAssembly in assembly.SubAssembly)
            {
                createStructure(subAssembly, layer + 1, position);
            }
        }


    }
}
