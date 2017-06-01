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

            getAssemblyCanvasStructure(m_factorioAssembly);
        }

        #endregion

        /// <summary>
        /// Used to create the Images and Lines that are drawn on the canvas.
        /// </summary>
        /// <param name="assembly">Assembly as tree root</param>
        /// <param name="layer">Current layer of the tree structure</param>
        /// <param name="position">Position of the Root element</param>
        /// <returns>The actuall position of the created AssemblyCanvasStructure</returns>
        private void getAssemblyCanvasStructure(FactorioAssembly assembly)
        {
            
        }

    }
}
