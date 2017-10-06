using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    /// <summary>
    /// This class represents one image in the prouction viewer
    /// </summary>
    public class PVImage : IPVImage
    {

        #region Private variables

        PVSettings m_settings;

        #endregion

        #region Interface Properties

        /// <summary>
        /// Width of the image
        /// </summary>
        public int ImageWidth
        {
            get { return this.ViewSettings.ImageWidth; }
        }

        /// <summary>
        /// Height of the image
        /// </summary>
        public int ImageHeight
        {
            get { return this.ViewSettings.ImageHeight; }
        }

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        public int Left { get; private set; }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top { get; private set; }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath { get; private set; }


        #endregion

        #region Properties

        /// <summary>
        /// Reference to the settings class
        /// </summary>
        public PVSettings ViewSettings
        {
            get { return m_settings; }
            private set { m_settings = value; }
        }

        /// <summary>
        /// Start position.
        /// The first position is 0
        /// </summary>
        public int PositionStart { get; set; }

        /// <summary>
        /// End position.
        /// The first position is 0
        /// </summary>
        public int PositionEnd { get; set; }

        /// <summary>
        /// Is true if this image is longer than 1 position
        /// </summary>
        public bool MultiPositionImage { get { return PositionStart != PositionEnd; } }

        /// <summary>
        /// Level of this image
        /// The first level is 0
        /// </summary>
        public int Level { get; set; }


        #endregion

        #region Constructions


        /// <summary>
        /// default consturctior
        /// </summary>
        /// <param name="assambly">this is the parrent assembly</param>
        /// <param name="setting">view settings class reference</param>
        public PVImage(string imagePath, int left, int top, PVSettings setting)
        {
            this.ImagePath = imagePath;
            this.Left = left;
            this.Top = top;
            this.ViewSettings = setting;
        }



        #endregion

        #region Private Methods



        #endregion
    }
}
