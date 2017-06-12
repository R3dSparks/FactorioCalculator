using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    /// <summary>
    /// This class represents one label which is shown next to an image in the production view
    /// </summary>
    public class PVLabel : IPVLabel
    {

        #region Private Variables



        private PVImage m_parentImage;
        private PVSettings m_settings;
        private FactorioAssembly m_relatedAssembly;



        #endregion

        #region Interface Properties



        /// <summary>
        /// Shown Text
        /// </summary>
        public string Text
        {
            get { return this.ParentAssembly.AssemblyItem.Name; }
        }

        /// <summary>
        /// Size of the text
        /// </summary>
        public int FontSize
        {
            get { return this.ViewSettings.LabelFontSize; }
        }

        /// <summary>
        /// Font familiy
        /// </summary>
        public string FontFamily
        {
            get { return this.ViewSettings.LabelFontFamily; }
        }

        /// <summary>
        /// Text color
        /// </summary>
        public Color FontColor
        {
            get { return this.ViewSettings.LabelFontColor; }
        }

        /// <summary>
        /// Background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return this.ViewSettings.LabelBackgroundColor; }
        }

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        public int Left
        {
            get
            {
                return this.ParentImage.Left + 200;
            }
        }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top
        {
            get
            {
                return this.ParentImage.Top + 200;
            }
        }



        #endregion

        #region Properties



        /// <summary>
        /// Public accessor of the parent image from this label
        /// </summary>
        public PVImage ParentImage
        {
            get { return m_parentImage; }
            private set { m_parentImage = value; }
        }

        /// <summary>
        /// Reference to the settings class
        /// </summary>
        public PVSettings ViewSettings
        {
            get { return m_settings; }
            private set { m_settings = value; }
        }

        /// <summary>
        /// Reference to the Assembly which is represented by this class
        /// </summary>
        public FactorioAssembly ParentAssembly
        {
            get { return m_relatedAssembly; }
            private set { m_relatedAssembly = value; }
        }



        #endregion

        #region Constructors



        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="image"></param>
        public PVLabel(PVImage image)
        {
            this.ParentImage = image;
            this.ViewSettings = this.ParentImage.ViewSettings;
            this.ParentAssembly = this.ParentImage.ParentAssembly;
        }



        #endregion

    }
}
