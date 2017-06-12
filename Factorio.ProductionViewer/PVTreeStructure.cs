using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper.ProductionViewer
{
    public class PVTreeStructure : IPVLogic
    {

        #region Private Variables



        private PVSettings m_settings;
        private FactorioAssembly m_parentAssembly;
        private List<IPVLine> m_lines;
        private List<IPVImage> m_images;



        #endregion

        #region Interface Properties



        /// <summary>
        /// images which are shown
        /// </summary>
        public List<IPVImage> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<IPVImage>();
                return m_images;
            }
            private set { m_images = value; }
        }

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
            private set { m_lines = value; }
        }



        #endregion

        #region Properties



        /// <summary>
        /// Settings for this prduction viewer
        /// </summary>
        public PVSettings Settings
        {
            get
            {
                if (m_settings == null)
                    m_settings = new PVSettings();
                return m_settings;
            }
        }

        /// <summary>
        /// Public accessor for the parent assembly of the tree structure
        /// </summary>
        public FactorioAssembly ParentAssembly
        {
            get { return m_parentAssembly; }
            private set { m_parentAssembly = value; }
        }



        #endregion

        #region Constructors



        /// <summary>
        /// create a new tree struction for the prorduction viewer and use the data from the parameter
        /// </summary>
        /// <param name="assembly">build a tree structure out of this data</param>
        public PVTreeStructure(FactorioAssembly assembly)
        {
            this.ParentAssembly = assembly;
            buildTreeStructure();
        }



        #endregion

        #region Public Methods



        /// <summary>
        /// rebuild this tree structure
        /// </summary>
        public void Rebuild()
        {
            this.Lines = new List<IPVLine>();
            this.Images = new List<IPVImage>();
            buildTreeStructure();
        }



        #endregion

        #region Private Methods



        /// <summary>
        /// build the tree structure out of the given assembly
        /// </summary>
        private void buildTreeStructure()
        {

        }


        #endregion



    }
}
