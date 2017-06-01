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
        /// <summary>
        /// Root assembly for this structure
        /// </summary>
        private FactorioAssembly m_assembly;

        private List<Layer> m_layers;

        public TreeStructure(FactorioAssembly assembly)
        {
            m_layers = new List<Layer>();

            createStructure(assembly, 0);
        }

        private void createStructure(FactorioAssembly assembly, int layer)
        {
            if (m_layers[layer] == null)
                m_layers[layer] = new Layer(layer);

            m_layers[layer].AddAssembly(assembly, 0);
        }


    }
}
