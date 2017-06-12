using Factorio;
using Factorio.Entities;
using Factorio.Entities.Interfaces.ProductionViewer;
using Factorio.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.ViewModels
{
    /// <summary>
    /// This view model provides the functionallity needed for the production view
    /// </summary>
    public class ProductionViewerViewModell : BaseViewModell
    {

        #region Private Variables



        private IFactorioLogic m_logic;

        private FactorioAssembly m_factorioAssembly;
        private List<IPVImage> m_images;
        private List<IPVLine> m_lines;

        private IPVLogic m_PVLogic;



        #endregion

        #region Public Properties



        /// <summary>
        /// 
        /// </summary>
        public List<IPVImage> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<IPVImage>();

                return m_images;
            }
        }

        /// <summary>
        /// 
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



        #endregion

        #region Constructor



        /// <summary>
        /// Create new ProductionView for an <see cref="FactorioItem"/>
        /// </summary>
        public ProductionViewerViewModell(IFactorioLogic logic, FactorioItem item)
        {
            m_logic = logic;

            m_factorioAssembly = new FactorioAssembly(item);
            
            m_PVLogic = new PVTreeStructure(m_factorioAssembly);

            m_images = m_PVLogic.Images;
            m_lines = m_PVLogic.Lines;
        }



        #endregion

    }
}
