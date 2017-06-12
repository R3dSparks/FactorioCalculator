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
                // get the distance for the starting position
                int firstPart = getDistance(this.PositionStart, true);

                // check if the image uses more than 1 position, if so than calculate the middle value of this distance
                int secondPart = this.MultiPositionImage ? getDistance(this.PositionEnd - this.PositionStart) / 2: 0;

                // add the distance of the starting point and the middle value if it takes more than one position
                return firstPart + secondPart;
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
            get { return this.ParentAssembly.AssemblyItem.ImagePath; }
        }


        #endregion

        #region Properties


        /// <summary>
        /// Reference to the Assembly which is represented by this class
        /// </summary>
        public FactorioAssembly ParentAssembly
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
            this.ParentAssembly = assambly;
            this.ViewSettings = setting;
        }



        #endregion

        #region Private Methods



        /// <summary>
        /// calculate the width distance value and add the margin if <paramref name="considerMargin"/> is true
        /// </summary>
        /// <param name="distance">position distances</param>
        /// <param name="considerMargin">if it is true, then it adds the margin of the tree structure at the end</param>
        /// <returns></returns>
        private int getDistance(int distance, bool considerMargin = false)
        {
            int firstPart = distance * this.ViewSettings.WidthOffset +          // calculate the width between all images befor this image
                    distance * this.ViewSettings.ImageWidth;                    // calculate the total image width of all images before this image
            
            int secondPart = considerMargin ? this.ViewSettings.MarginLeft : 0; // add the left margin of the tree structure if considerMargin is true

            return firstPart + secondPart;
        }



        #endregion

    }
}
