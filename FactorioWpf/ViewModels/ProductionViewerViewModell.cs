using Factorio;
using Factorio.Entities;
using Factorio.Entities.Enum;
using Factorio.Entities.Interfaces.ProductionViewer;
using Factorio.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private ObservableCollection<IPVFactorioItemContainer> m_factorioItemContainers;
        private List<IPVLine> m_lines;

        private IPVLogic m_PVLogic;

        #endregion

        #region Public Properties

        public IPVLogic PVLogic
        {
            get
            {
                return m_PVLogic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<IPVFactorioItemContainer> FactorioItemContainers
        {
            get
            {
                if (m_factorioItemContainers == null)
                    m_factorioItemContainers = new ObservableCollection<IPVFactorioItemContainer>();

                return m_factorioItemContainers;
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

            m_factorioItemContainers = m_PVLogic.FactorioItemContainers;
            m_lines = m_PVLogic.Lines;
        }



        #endregion

    }
}
