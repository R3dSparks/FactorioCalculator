using Factorio;
using Factorio.Entities;
using Factorio.Entities.Interfaces.ProductionViewer;
using Factorio.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioWpf.ViewModels
{
    /// <summary>
    /// This view model provides the functionallity needed for the production view
    /// </summary>
    public class ProductionViewerViewModell : BaseViewModell
    {

        #region Private Variables



        private IFactorioLogic m_logic;
        private IPVLogic m_PVLogic;

        private FactorioAssembly m_factorioAssembly;
        private List<IPVBaseNode> m_nodes;



        #endregion

        #region Public Properties

        /// <summary>
        /// all shown items
        /// </summary>
        public List<IPVBaseNode> Nodes
        {
            get
            {
                if (m_nodes == null)
                    m_nodes = new List<IPVBaseNode>();
                return m_nodes;
            }
            private set { m_nodes = value; }
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

            this.Nodes = m_PVLogic.Nodes;
        }



        #endregion

    }
}
