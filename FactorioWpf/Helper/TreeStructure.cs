using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class TreeStructure : IPVLogic
    {

        #region Private Variables


        private List<Layer> m_layers;
        private List<IPVLine> m_lines;



        #endregion

        #region Properties



        /// <summary>
        /// lines which connects the pictures
        /// </summary>
        public List<IPVLine> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<IPVLine>();

                return m_lines;
            }
        }

        /// <summary>
        /// images which are shown
        /// </summary>
        public List<IPVImage> Images
        {
            get
            {
                List<IPVImage> images = new List<IPVImage>();

                foreach (var layer in m_layers)
                {
                    images.AddRange(layer.AssemblyImages);
                }

                return images;
            }
        }



        #endregion

        #region Constructors


        
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="assembly"></param>
        public TreeStructure(FactorioAssembly assembly)
        {
            m_layers = new List<Layer>(1);

            createLayer(assembly, 0, 0);
        }



        #endregion

        #region Private Method
        


        /// <summary>
        /// creates the structure for the production view
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="layer"></param>
        /// <param name="position"></param>
        /// <returns></returns>
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



        #endregion

        
    }
}
