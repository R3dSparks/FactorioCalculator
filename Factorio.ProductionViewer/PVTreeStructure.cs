using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    public class PVTreeStructure : IPVLogic
    {

        #region Private Variables


        private PVSettings m_settings;
        private FactorioAssembly m_parentAssembly;
        private List<IPVLine> m_lines;
        private List<IPVFactorioItemContainer> m_factorioItemContainers;


        #endregion

        #region Interface Properties

        /// <summary>
        /// images which are shown
        /// </summary>
        public List<IPVFactorioItemContainer> FactorioItemContainers
        {
            get
            {
                if (m_factorioItemContainers == null)
                    m_factorioItemContainers = new List<IPVFactorioItemContainer>();
                return m_factorioItemContainers;
            }
            private set { m_factorioItemContainers = value; }
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
            buildTreeStructure(this.ParentAssembly);
        }



        #endregion

        #region Public Methods



        /// <summary>
        /// rebuild this tree structure
        /// </summary>
        public void Rebuild()
        {
            this.Lines = new List<IPVLine>();
            this.FactorioItemContainers = new List<IPVFactorioItemContainer>();
            buildTreeStructure(this.ParentAssembly);
        }



        #endregion

        #region Private Methods



        /// <summary>
        /// build the tree structure out of the given assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="position"></param>
        /// <param name="level"></param>
        private PVFactorioItemContainer buildTreeStructure(FactorioAssembly assembly, int position = 0, int level = 0)
        {
            PVFactorioItemContainer container = new PVFactorioItemContainer(assembly, level, this.Settings);

            container.Left = position;

            int newPosition = position;

            for(int i=0; i < container.Assembly.SubAssembly.Count; i++)
            {
                // Build a branch for the current subassembly and position it right to the last subassembly
                PVFactorioItemContainer currentContainer = buildTreeStructure(container.Assembly.SubAssembly[i], position + i * (Settings.ItemContainerWidth + Settings.WidthOffset), level + 1);

                Lines.Add(new PVLine(container, currentContainer));

                newPosition = (currentContainer.Left - position) / 2;
            }

            container.Left = newPosition;

            FactorioItemContainers.Add(container);

            return container;
        }

        


        #endregion
        
    }
}
