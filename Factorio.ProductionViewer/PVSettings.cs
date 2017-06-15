using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
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



        // item placeholder values
        private int m_iphWidth = 50;
        private int m_iphHeight = 50;
        private int m_iphColumns = 1;
        private int m_iphRows = 2;

        // image values
        private int m_imageWidth = 30;
        private int m_imageHeight = 30;
        private Margins m_imageMargin = new Margins(5, 5, 5, 5);


        // space between items
        private int m_heightOffset = 60;
        private int m_widthOffset = 40;


        // tree structure magine
        private Margins m_treeStructureMagin = new Margins(20, 0, 20, 0);


        // label values
        private PVLabelLocation m_labelLocation = PVLabelLocation.Sourth;
        private int m_labelFontSize = 12;
        private string m_labelFontFamily = "Arial";
        private Color m_labelFontColor = Color.Black;
        private Color m_labelBackgroundColor = Color.Transparent;
        private Margins m_labelMargin = new Margins(5, 5, 5, 5);



        #endregion

        #region Item Placeholder Properties



        /// <summary>
        /// Minimum width for an item place holder
        /// </summary>
        public int ItemPHMinWidth { get; set; }

        /// <summary>
        /// Minimum height for an item place holder
        /// </summary>
        public int ItemPHMinHeight { get; set; }

        /// <summary>
        /// Columns in the item placeholders.
        /// <para></para>
        /// The value must be at least 1 or gather
        /// </summary>
        public int ItemPHColumns { get; set; }

        /// <summary>
        /// Rows in the item placeholders.
        /// <para></para>
        /// The value must be at least 1 or gather
        /// </summary>
        public int ItemPHRows { get; set; }



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

        /// <summary>
        /// Margins of an image
        /// </summary>
        public Margins ImageMargin { get; set; }



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
        /// tree structure margins
        /// </summary>
        public Margins TreeStructureMargin { get; set; }



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

        /// <summary>
        /// Label margins
        /// </summary>
        public Margins LabelMargin { get; set; }



        #endregion

        #region Consturctor



        /// <summary>
        /// default constructor. It uses the default values
        /// </summary>
        public PVSettings()
        {
            this.ItemPHMinHeight = m_iphHeight;
            this.ItemPHMinWidth = m_iphWidth;
            this.ItemPHColumns = m_iphColumns;
            this.ItemPHRows = m_iphRows;

            this.ImageHeight = m_imageHeight;
            this.ImageWidth = m_imageWidth;
            this.ImageMargin = m_imageMargin;

            this.HeightOffset = m_heightOffset;
            this.WidthOffset = m_widthOffset;

            this.TreeStructureMargin = m_treeStructureMagin;

            this.LabelLocation = m_labelLocation;
            this.LabelFontSize = m_labelFontSize;
            this.LabelFontFamily = m_labelFontFamily;
            this.LabelFontColor = m_labelFontColor;
            this.LabelBackgroundColor = m_labelBackgroundColor;
            this.LabelMargin = m_labelMargin;
        }



        #endregion
      
    }
}
