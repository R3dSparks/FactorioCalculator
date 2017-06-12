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


        FactorioAssembly m_relatedAssembly;
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
        public int Left
        {
            get
            {
                return this.PositionStart * this.ViewSettings.WidthOffset + // calculate the width between all images befor this image
                    this.PositionStart * this.ViewSettings.ImageWidth +     // calculate the total image width of all images before this image
                    this.ViewSettings.MarginLeft;                           // add the left margin of the tree structure
            }
        }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top
        {
            get
            {
                return this.Level * this.ViewSettings.HeightOffset +    // calculate the height between all images before this image
                    this.Level * this.ViewSettings.ImageHeight +        // calculate the total image height of all images before this image
                    this.ViewSettings.MarginTop;                        // add the top margin of the tree structure
            }
        }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath
        {
            get { return this.PatrentAssembly.AssemblyItem.ImagePath; }
        }


        #endregion

        #region Properties


        /// <summary>
        /// Reference to the Assembly which is represented by this class
        /// </summary>
        public FactorioAssembly PatrentAssembly
        {
            get { return m_relatedAssembly; }
            private set { m_relatedAssembly = value; }
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
        public PVImage(FactorioAssembly assambly, PVSettings setting)
        {
            this.PatrentAssembly = assambly;
            this.ViewSettings = setting;
        }



        #endregion


    }
}
