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
        private List<AssemblyImageHelper> m_images;
        private List<Line> m_lines;

        private TreeStructure m_assemblyStructure;

        #endregion

        #region Public Properties

        public List<AssemblyImageHelper> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<AssemblyImageHelper>();

                return m_images;
            }
        }

        public List<Line> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<Line>();

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

            m_assemblyStructure = new TreeStructure(m_factorioAssembly);

            m_images = m_assemblyStructure.GetImageList();
            m_lines = m_assemblyStructure.Lines;
        }

        #endregion

    }
}
