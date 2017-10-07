using Factorio.Entities.Enum;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    public class PVFactorioItemContainer : IPVFactorioItemContainer, INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Private Variables

        private FactorioAssembly m_assembly;
        private PVSettings m_settings;
        private PVImage m_image;
        private List<CraftingStation> m_assemblyOptions;
        private CraftingStation m_selectedCraftingStation;

        #endregion

        #region Public Properties

        public CraftingStation SelectedCraftingStation
        {
            get
            {
                return m_selectedCraftingStation;
            }
            set
            {
                m_selectedCraftingStation = value;
                Assembly.CraftingStation = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Information"));
            }
        }

        public string Information
        {
            get
            {
                return $"Quantity: {this.Assembly.Quantity}\nProductivity: {this.Assembly.AssemblyItem.Productivity}";
            }
        }

        public PVSettings Settings
        {
            get
            {
                return m_settings;
            }
        }

        public int Width { get; set; }

        public FactorioAssembly Assembly
        {
            get
            {
                return m_assembly;
            }
        }

        public int Level { get; set; }

        #endregion

        #region Interface Implementation

        public List<CraftingStation> AssemblyOptions
        {
            get
            {
                if (m_assemblyOptions == null)
                    m_assemblyOptions = FactorioHelper.GetCraftingStationsFromCraftingType(this.m_assembly.AssemblyItem.DefaultCraftingType);

                return m_assemblyOptions;
            }
        }

        public IPVImage Image
        {
            get
            {
                if (m_image == null)
                    m_image = new PVImage(
                        this.Assembly.AssemblyItem.ImagePath,
                        this.Left + this.m_settings.ImageLeftOffset,
                        this.Top + this.m_settings.ImageTopOffset,
                        this.m_settings);

                return m_image;
            }
        }

        public int ContainerHeight
        {
            get
            {
                return this.m_settings.ItemContainerHeight;
            }
        }

        public int ContainerWidth
        {
            get
            {
                return this.m_settings.ItemContainerWidth;
            }
        }

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top
        {
            get
            {
                return this.Level * this.m_settings.HeightOffset +    // calculate the height between all images before this image
                    this.Level * this.m_settings.ImageHeight +        // calculate the total image height of all images before this image
                    this.m_settings.MarginTop;                        // add the top margin of the tree structure
            }
        }

        #endregion

        #region Constructors

        public PVFactorioItemContainer(FactorioAssembly assembly, int level, PVSettings settings)
        {
            this.m_assembly = assembly;
            this.Level = level;
            this.m_settings = settings;
        }

        #endregion

    }
}
