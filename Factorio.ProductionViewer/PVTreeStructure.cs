using Factorio.Entities;
using Factorio.Entities.Interfaces;
using Factorio.Entities.Interfaces.ProductionViewer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Factorio.ProductionViewer
{
    public class PVTreeStructure : IPVLogic, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Private Variables


        private PVSettings m_settings;
        private List<IPVLine> m_lines;
        private List<IPVFactorioItemContainer> m_factorioItemContainers;
        private PVFactorioItemContainer m_rootContainer;


        #endregion

        #region Interface Properties

        public IPVFactorioItemContainer RootContainer
        {
            get
            {
                return m_rootContainer;
            }
        }

        /// <summary>
        /// List off all <see cref="PVFactorioItemContainer"/>
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
        /// Lines which connects the pictures
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

        /// <summary>
        /// Total height of the tree
        /// </summary>
        public int TotalHeight
        {
            get
            {
                int height = 0;

                foreach (var container in FactorioItemContainers)
                {
                    if (container.Top > height)
                        height = container.Top;
                }

                return height + Settings.ItemContainerHeight + Settings.HeightOffset;
            }
        }

        /// <summary>
        /// Total width of the tree
        /// </summary>
        public int TotalWidth
        {
            get
            {
                return FactorioItemContainers[FactorioItemContainers.Count - 1].SubTreeWidth;
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


        #endregion

        #region Constructors

        /// <summary>
        /// Create a new tree structior for the production viewer and use the data from the parameter
        /// </summary>
        /// <param name="assembly">Build a tree structure from this data</param>
        public PVTreeStructure(IFactorioAssembly assembly)
        {
            buildTreeStructure(assembly);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// build the tree structure out of the given assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="position"></param>
        /// <param name="level"></param>
        private PVFactorioItemContainer buildTreeStructure(IFactorioAssembly assembly, int position = 0, int level = 0)
        {
            PVFactorioItemContainer container = new PVFactorioItemContainer(assembly, level, this.Settings, this.FactorioItemContainers);

            if (level == 0)
                m_rootContainer = container;

            container.Left = position;
            container.SubTreeWidth = 0;

            PVFactorioItemContainer firstSubContainer = null;
            PVFactorioItemContainer lastSubContainer = null;

            for(int i=0; i < container.Assembly.SubAssembly.Count; i++)
            {
                // Build a branch for the current subassembly and position it right to the last subassembly
                PVFactorioItemContainer currentSubContainer = buildTreeStructure(
                    container.Assembly.SubAssembly[i],
                    container.Left + container.SubTreeWidth,
                    level + 1);

                if (firstSubContainer == null)
                    firstSubContainer = currentSubContainer;

                lastSubContainer = currentSubContainer;

                Lines.Add(new PVLine(container, currentSubContainer));

                container.SubTreeWidth += currentSubContainer.SubTreeWidth;
            }

            if(firstSubContainer != null)
            {
                container.Left = (lastSubContainer.Left - firstSubContainer.Left) / 2 + firstSubContainer.Left;
            }
            else
                container.SubTreeWidth = this.Settings.ItemContainerWidth + this.Settings.WidthOffset;



            FactorioItemContainers.Add(container);

            return container;
        }

        


        #endregion
        
    }
}
