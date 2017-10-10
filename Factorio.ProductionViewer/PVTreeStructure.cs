using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factorio.ProductionViewer
{
    public class PVTreeStructure : IPVLogic
    {

        #region Private Variables


        private PVSettings m_settings;
        private FactorioAssembly m_parentAssembly;
        private List<IPVLine> m_lines;
        private ObservableCollection<IPVFactorioItemContainer> m_factorioItemContainers;
        private IPVFactorioItemContainer m_rootContainer;


        #endregion

        #region Interface Properties

        /// <summary>
        /// images which are shown
        /// </summary>
        public ObservableCollection<IPVFactorioItemContainer> FactorioItemContainers
        {
            get
            {
                if (m_factorioItemContainers == null)
                    m_factorioItemContainers = new ObservableCollection<IPVFactorioItemContainer>();
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

        public IPVFactorioItemContainer RootContainer
        {
            get
            {
                return m_rootContainer;
            }
        }

        /// <summary>
        /// Total width of the tree
        /// </summary>
        public int TotalWidth
        {
            get
            {
                return FactorioItemContainers[FactorioItemContainers.Count - 1].Width;
            }
        }

        /// <summary>
        /// Margin for tree structure inside the canvas
        /// </summary>
        public Thickness Margin
        {
            get
            {
                return new Thickness(Settings.MarginLeft, Settings.MarginTop, 0, 0);
            }
        }

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
            this.FactorioItemContainers = new ObservableCollection<IPVFactorioItemContainer>();
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
            PVFactorioItemContainer container = new PVFactorioItemContainer(assembly, level, this.Settings, this);

            if (level == 0)
                m_rootContainer = container;

            container.Left = position;
            container.Width = 0;

            PVFactorioItemContainer firstSubContainer = null;
            PVFactorioItemContainer lastSubContainer = null;

            for(int i=0; i < container.Assembly.SubAssembly.Count; i++)
            {
                // Build a branch for the current subassembly and position it right to the last subassembly
                PVFactorioItemContainer currentSubContainer = buildTreeStructure(
                    container.Assembly.SubAssembly[i],
                    container.Left + container.Width,
                    level + 1);

                if (firstSubContainer == null)
                    firstSubContainer = currentSubContainer;

                lastSubContainer = currentSubContainer;

                Lines.Add(new PVLine(container, currentSubContainer));

                container.Width += currentSubContainer.Width;
            }

            if(firstSubContainer != null)
            {
                container.Left = (lastSubContainer.Left - firstSubContainer.Left) / 2 + firstSubContainer.Left;
            }
            else
                container.Width = this.Settings.ItemContainerWidth + this.Settings.WidthOffset;



            FactorioItemContainers.Add(container);

            return container;
        }

        


        #endregion
        
    }
}
