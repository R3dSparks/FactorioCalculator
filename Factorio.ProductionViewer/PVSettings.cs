using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    /// <summary>
    /// This class contain values which are used in calculating the view for the production viewer
    /// </summary>
    public class PVSettings
    {

        #region Default Values


        private int m_imageWidth = 60;
        private int m_imageHeight = 40;

        private int m_heightOffset = 40;
        private int m_widthOffset = 20;

        private int m_marginTop = 20;
        private int m_marginLeft = 20;


        #endregion
        
        #region Properties



        /// <summary>
        /// The width of all images in the production view
        /// </summary>
        public int ImageWidth { get; set; }

        /// <summary>
        /// The hight of all images in the production view
        /// </summary>
        public int ImageHeight { get; set; }

        /// <summary>
        /// Space in the height between pictures
        /// </summary>
        public int HeightOffset { get; set; }

        /// <summary>
        /// space in the with between pirctures
        /// </summary>
        public int WidthOffset { get; set; }

        /// <summary>
        /// top margin for the tree structure
        /// </summary>
        public int MarginTop { get; set; }

        /// <summary>
        /// left margin for the tree structure
        /// </summary>
        public int MarginLeft{ get; set; }



        #endregion

        #region Consturctor



        /// <summary>
        /// default constructor. It uses the default values
        /// </summary>
        public PVSettings()
        {
            this.ImageHeight = m_imageHeight;
            this.ImageWidth = m_imageWidth;
            this.HeightOffset = m_heightOffset;
            this.WidthOffset = m_widthOffset;
            this.MarginTop = m_marginTop;
            this.MarginLeft = m_marginLeft;
        }

        /// <summary>
        /// constructor with defined values for all settings
        /// </summary>
        /// <param name="imageHight">heightr of an image</param>
        /// <param name="imageWidth">width of an image</param>
        /// <param name="heightOffset">space between images in the hight</param>
        /// <param name="widthOffset">space between images in the width</param>
        /// <param name="marginTop">margin to the top side</param>
        /// <param name="marginLeft">margin on the left side</param>
        public PVSettings(int imageHight, int imageWidth, int heightOffset, int widthOffset, int marginTop, int marginLeft)
        {
            this.ImageHeight = imageHight;
            this.ImageWidth = imageWidth;
            this.HeightOffset = heightOffset;
            this.WidthOffset = widthOffset;
            this.MarginTop = marginTop;
            this.MarginLeft = marginLeft;
        }



        #endregion
        
    }
}
