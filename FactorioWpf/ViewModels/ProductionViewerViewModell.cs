using Factorio;
using Factorio.Entities;
using Factorio.Entities.Interfaces.ProductionViewer;
using Factorio.ProductionViewer;
using System;
using System.Collections.Generic;

namespace FactorioWpf.ViewModels
{
    /// <summary>
    /// This view model provides the functionality needed for the production view
    /// </summary>
    public class ProductionViewerViewModell : BaseViewModell
    {

        #region Private Variables

        private FactorioAssembly m_factorioAssembly;
        private List<IPVFactorioItemContainer> m_factorioItemContainers;
        private List<IPVLine> m_lines;

        private IPVLogic m_PVLogic;

        #endregion

        #region Public Properties

        /// <summary>
        /// Contains all information about the assembly tree
        /// </summary>
        public IPVLogic PVLogic
        {
            get
            {
                return m_PVLogic;
            }
        }

        /// <summary>
        /// Quantity of crafting stations for the root assembly
        /// </summary>
        public string RootQuantity
        {
            get
            {
                return m_PVLogic.RootContainer.Quantity.ToString();
            }
            set
            {
                m_PVLogic.RootContainer.Quantity = Convert.ToDouble(value);
            }
        }

        /// <summary>
        /// List with all tree nodes
        /// </summary>
        public List<IPVFactorioItemContainer> FactorioItemContainers
        {
            get
            {
                if (m_factorioItemContainers == null)
                    m_factorioItemContainers = new List<IPVFactorioItemContainer>();

                return m_factorioItemContainers;
            }
        }

        /// <summary>
        /// List with all tree lines
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
        public ProductionViewerViewModell(FactorioItem item)
        {
            m_factorioAssembly = new FactorioAssembly(item);
            m_PVLogic = new PVTreeStructure(m_factorioAssembly);

            m_factorioItemContainers = m_PVLogic.FactorioItemContainers;
            m_lines = m_PVLogic.Lines;
        }



        #endregion

    }
}
