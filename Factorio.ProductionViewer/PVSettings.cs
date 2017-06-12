using System;
using System.Collections.Generic;
using System.Drawing;
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


        // image values
        private int m_imageWidth = 30;
        private int m_imageHeight = 30;


        // space between items
        private int m_heightOffset = 40;
        private int m_widthOffset = 20;


        // tree structure magine
        private int m_marginTop = 20;
        private int m_marginLeft = 20;


        // label values
        private PVLabelLocation m_labelLocation = PVLabelLocation.Sourth;
        private int m_labelFontSize = 12;
        private string m_labelFontFamily = "Arial";
        private Color m_labelFontColor = Color.Black;
        private Color m_labelBackgroundColor = Color.Transparent;
        


        #endregion
        
        #region Image Properties



        /// <summary>
        /// The width of all images in the production view
        /// </summary>
        public int ImageWidth { get; set; }

        /// <summary>
        /// The hight of all images in the production view
        /// </summary>
        public int ImageHeight { get; set; }



        #endregion

        #region Space between Items Properties



        /// <summary>
        /// Space in the height between pictures
        /// </summary>
        public int HeightOffset { get; set; }

        /// <summary>
        /// space in the with between pirctures
        /// </summary>
        public int WidthOffset { get; set; }



        #endregion

        #region Tree Structure Margin Properties



        /// <summary>
        /// top margin for the tree structure
        /// </summary>
        public int MarginTop { get; set; }

        /// <summary>
        /// left margin for the tree structure
        /// </summary>
        public int MarginLeft{ get; set; }



        #endregion

        #region Label Properties



        /// <summary>
        /// Label location relative to its related image
        /// </summary>
        public PVLabelLocation LabelLocation { get; set; }

        /// <summary>
        /// Size of the label text
        /// </summary>
        public int LabelFontSize { get; set; }

        /// <summary>
        /// Label font familiy
        /// </summary>
        public string LabelFontFamily { get; set; }

        /// <summary>
        /// Label text color
        /// </summary>
        public Color LabelFontColor { get; set; }

        /// <summary>
        /// Label background color
        /// </summary>
        public Color LabelBackgroundColor { get; set; }



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

            this.LabelLocation = m_labelLocation;
            this.LabelFontSize = m_labelFontSize;
            this.LabelFontFamily = m_labelFontFamily;
            this.LabelFontColor = m_labelFontColor;
            this.LabelBackgroundColor = m_labelBackgroundColor;
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
        public PVSettings(int imageHight, int imageWidth, int heightOffset, int widthOffset, int marginTop, int marginLeft) : this()
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
