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

        List<ImageHelper> Images
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

            Images.Add(new ImageHelper(
                    10,
                    10,
                    m_factorioAssembly.AssemblyItem.PicturePath
                ));
        }

        #endregion

    }
}
