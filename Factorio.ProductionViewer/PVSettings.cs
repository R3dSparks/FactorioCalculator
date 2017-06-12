using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper.ProductionViewer
{
    /// <summary>
    /// This class contain values which are used in calculating the view for the production viewer
    /// </summary>
    public class PVSettings
    {

        #region Default Values


        private int m_imageWidth = 30;
        private int m_imageHeight = 30;
        private int m_heightOffset = 40;
        private int m_widthOffset = 20;


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
        }

        /// <summary>
        /// constructor with defined values for all settings
        /// </summary>
        /// <param name="imageHight"></param>
        /// <param name="imageWidth"></param>
        /// <param name="heightOffset"></param>
        /// <param name="widthOffset"></param>
        public PVSettings(int imageHight, int imageWidth, int heightOffset, int widthOffset)
        {
            this.ImageHeight = imageHight;
            this.ImageWidth = imageWidth;
            this.HeightOffset = heightOffset;
            this.WidthOffset = widthOffset;
        }


        #endregion
        
    }
}
