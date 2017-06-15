using Factorio.Entities.Interfaces.ProductionViewer;
using Factorio.ProductionViewer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace Factorio.ProductionViewer
{
    /// <summary>
    /// This class represents one image in the prouction viewer
    /// </summary>
    public class PVImage : IPVImage, IPVItem
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



                //// get the distance for the starting position
                //int firstPart = getDistance(this.PositionStart, true);

                //// check if the image uses more than 1 position, if so than calculate the middle value of this distance
                //int secondPart = this.MultiPositionImage ? getDistance(this.PositionEnd - this.PositionStart) / 2: 0;

                //// add the distance of the starting point and the middle value if it takes more than one position
                //return firstPart + secondPart;
            }
        }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top
        {
            get
            {
                //return this.Level * this.ViewSettings.HeightOffset +    // calculate the height between all images before this image
                //    this.Level * this.ViewSettings.ImageHeight +        // calculate the total image height of all images before this image
                //    this.ViewSettings.TreeStructureMargin.Top;          // add the top margin of the tree structure
            }
        }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath
        {
            get { return this.ParentAssembly.AssemblyItem.ImagePath; }
        }

        /// <summary>
        /// Name of the assembly which can be seen in the image
        /// </summary>
        public string AssemblyName
        {
            get { return this.ParentAssembly.AssemblyItem.Name; }
        }



        #endregion

        #region IPVItem Properties



        /// <summary>
        /// Height of the image
        /// </summary>
        public int Height
        {
            get { return this.ViewSettings.ImageHeight; }
        }

        /// <summary>
        /// Width of the image
        /// </summary>
        public int Width
        {
            get { return this.ViewSettings.ImageWidth; }
        }

        /// <summary>
        /// Margin of the item
        /// </summary>
        public Margins Margin
        {
            get { return this.ViewSettings.ImageMargin; }
        }

        /// <summary>
        /// Column position of this image.
        /// <para>
        /// The first coulmn is 0.
        /// </para>
        /// If a item already exists in that position value, this one gets ignored.
        /// If the <see cref="PVItemPlaceHolder"/> does not have a grid with this column number it also get ignored.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Row position of this image.
        /// <para>
        /// The first row is 0.
        /// </para>
        /// If a item already exists in that position value, this one gets ignored.
        /// If the <see cref="PVItemPlaceHolder"/> does not have a grid with this row number it also get ignored.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Reference to the placeholder where the image is located
        /// </summary>
        public PVItemPlaceHolder ParentPlaceHolder { get; set; }



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
            
            int secondPart = considerMargin ? this.ViewSettings.TreeStructureMargin.Left : 0; // add the left margin of the tree structure if considerMargin is true

            return firstPart + secondPart;
        }



        #endregion

    }
}
