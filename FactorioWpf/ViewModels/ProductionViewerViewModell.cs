using Factorio;
using Factorio.Entities;
using FactorioWpf.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.ViewModels
{
    public class ProductionViewerViewModell : BaseViewModell
    {
        #region Private Variables

        private IFactorioLogic m_logic;

        private FactorioAssembly m_factorioAssembly;
        private List<ImageHelper> m_images;

        #endregion

        #region Public Properties

        public List<ImageHelper> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<ImageHelper>();

                return m_images;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Create new ProductionView for an <see cref="FactorioItem"/>
        /// </summary>
        public ProductionViewerViewModell(IFactorioLogic logic, FactorioItem item)
        {
            m_logic = logic;

            m_factorioAssembly = new FactorioAssembly(item);

            getAssembly(m_factorioAssembly, 0, 0);
        }

        #endregion

        private void getAssembly(FactorioAssembly assembly, int layer, int pos)
        {
            Images.Add(new ImageHelper(pos * 50, layer * 70, assembly.AssemblyItem.PicturePath));

            int nextPos = pos;

            foreach (FactorioAssembly subAssembly in assembly.SubAssembly)
            {
                getAssembly(subAssembly, layer + 1, nextPos);

                nextPos++;
            }
        }

    }
}
