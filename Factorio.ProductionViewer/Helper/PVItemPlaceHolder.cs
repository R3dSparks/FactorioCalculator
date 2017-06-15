using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer.Helper
{
    /// <summary>
    /// This class is used to store and order shown items
    /// </summary>
    public class PVItemPlaceHolder : IRestable
    {

        #region Private Variables



        private List<IPVItem> m_items;
        private List<PVColumn> m_columns;
        PVSettings m_settings;
        private int m_top;
        private int m_left;
        private int m_width;
        private int m_height;
        private List<int> m_widthPerColumn;
        private List<int> m_heightPerRow;



        #endregion

        #region Properties



        /// <summary>
        /// contains all items which are located in this <see cref="PVItemPlaceHolder"/>
        /// </summary>
        public List<IPVItem> Items
        {
            get
            {
                if (m_items == null)
                    m_items = new List<IPVItem>();
                return m_items;
            }
            private set { m_items = value; }
        }

        /// <summary>
        /// Space between the canvas and the top of this item placeholder
        /// </summary>
        public int Top
        {
            get
            {
                if (m_top == 0)
                    m_top = calculateTop();
                return m_top;
            }
            private set { m_top = value; }
        }

        /// <summary>
        /// Space between the canvas and the left side of this item palceholder
        /// </summary>
        public int Left
        {
            get
            {
                if (m_left == 0)
                    m_left = calculateLeft();
                return m_left;
            }
            private set { m_left = value; }
        }

        /// <summary>
        /// Total width of this item placeholder
        /// </summary>
        public int Width
        {
            get
            {
                if (m_width == 0)
                    m_width = calculateWidth();
                return m_width;
            }
            private set { m_width = value; }
        }

        /// <summary>
        /// Total height of this item placeholder
        /// </summary>
        public int Height
        {
            get
            {
                if (m_height == 0)
                    m_height = calculateHeight();
                return m_height;
            }
            private set { m_height = value; }
        }

        /// <summary>
        /// Withs of the columns
        /// </summary>
        public List<int> ColumnWidths
        {
            get
            {
                if (m_widthPerColumn == null)
                    calculateWidth();
                return m_widthPerColumn;
            }
            private set { m_widthPerColumn = value; }
        }

        /// <summary>
        /// Heights of the rows
        /// </summary>
        public List<int> RowHeights
        {
            get
            {
                if (m_heightPerRow == null)
                    calculateHeight();
                return m_heightPerRow;
            }
            private set { m_heightPerRow = value; }
        }

        /// <summary>
        /// Related Row
        /// </summary>
        public PVRow ParentRow { get; set; }

        /// <summary>
        /// Related Columns
        /// </summary>
        public List<PVColumn> ParentColumns
        {
            get
            {
                if (m_columns == null)
                    m_columns = new List<PVColumn>();
                return m_columns;
            }
            private set { m_columns = value; }
        }

        /// <summary>
        /// Reference to the settings class
        /// </summary>
        public PVSettings Settings
        {
            get { return m_settings; }
            private set { m_settings = value; }
        }


        #endregion

        #region Constructors



        /// <summary>
        /// Create a new item palceholder and reference the settings class
        /// </summary>
        /// <param name="settings"></param>
        public PVItemPlaceHolder(PVSettings settings)
        {
            this.Settings = settings;
        }



        #endregion

        #region Interface Methods



        /// <summary>
        /// Reset the <see cref="Top"/>, <see cref="Left"/>, <see cref="Height"/>, <see cref="Width"/>, <see cref="ColumnWidths"/> and <see cref="RowHeights"/> values.
        /// </summary>
        public void RestFixedValues()
        {
            this.Top = 0;
            this.Left = 0;
            this.Height = 0;
            this.Width = 0;
            this.ColumnWidths = null;
            this.RowHeights = null;
        }


        #endregion

        #region Public Methods



        /// <summary>
        /// Add the item to this placeholder and create the relashion betwenn the classes
        /// </summary>
        /// <param name="item">add this item</param>
        public void AddItem(IPVItem item)
        {
            this.Items.Add(item);
            item.ParentPlaceHolder = this;
        }



        #endregion

        #region Private Method



        /// <summary>
        /// Calculate the left distance between the canvas and this item placeholder
        /// </summary>
        /// <returns></returns>
        private int calculateLeft()
        {
            // find min and max index
            int minIndex = this.ParentColumns.Min(x => x.Index);
            int maxIndex = this.ParentColumns.Max(x => x.Index);


            // find first and last column
            var firstColumn = this.ParentColumns.Where(x => x.Index == minIndex).FirstOrDefault();
            var lastColumn = this.ParentColumns.Where(x => x.Index == maxIndex).FirstOrDefault();


            // stop if there was a problem
            if (firstColumn == null || lastColumn == null)
                return 0;


            // get the space between the left side of the canvas and the firstColumn
            int fixedSpace = addColumnWidth(firstColumn.PreviousColumn) +
                this.Settings.WidthOffset * firstColumn.Index +
                this.Settings.TreeStructureMargin.Left;


            // get the availible column space
            int availibleColumnSpace = addColumnWidth(lastColumn, firstColumn) +
                this.Settings.WidthOffset * (lastColumn.Index - firstColumn.Index);


            // 
            return fixedSpace + (availibleColumnSpace / 2) - this.Width;
        }

        /// <summary>
        /// Calculate the top distance between the canvas and this item placeholder
        /// </summary>
        /// <returns></returns>
        private int calculateTop()
        {
            // calculate the total hight of all rows above and add the total space between the rows
            return addRowHeights(this.ParentRow) +
                this.Settings.HeightOffset * this.ParentRow.Index +
                this.Settings.TreeStructureMargin.Top;
        }

        /// <summary>
        /// Add all row hights togather
        /// </summary>
        /// <param name="row">this is the next row which get added</param>
        /// <returns>returns the total hight of the rows</returns>
        private int addRowHeights(PVRow row)
        {
            int previousRowHeight = 0;

            // if there is a previous row add it.
            if (row.PreviousRow != null)
                previousRowHeight = addRowHeights(row.PreviousRow);

            return row.Height + previousRowHeight;
        }

        /// <summary>
        /// Add all column width togather until it reaches the stop column.
        /// </summary>
        /// <param name="column">this is the next column wich get added</param>
        /// <param name="stopColumn">this is the last column which is considert for the calculation</param>
        /// <returns>returns the total width of all considert columns</returns>
        private int addColumnWidth(PVColumn column, PVColumn stopColumn = null)
        {
            int previousColumnWidth = 0;


            // check if the column is null and return 0 if this is true
            if (column == null)
                return 0;


            // check if the current column is the column where the calculation is supposed to stop.
            // if this is true it returns the width of the current column
            if (stopColumn == column)
                return column.Width;


            // check if there is a previous column.
            // if this is true it calls this function again with the previous column
            if (column.PreviousColumn != null)
                previousColumnWidth = addColumnWidth(column.PreviousColumn, stopColumn);


            // starts adding all values togather
            return column.Width + previousColumnWidth;
        }

        /// <summary>
        /// calculate the width of this item place holder and save all column widths in <see cref="m_widthPerColumn"/>.
        /// </summary>
        /// <returns></returns>
        private int calculateWidth()
        {
            // reset the list which contains the calculated width per column
            this.ColumnWidths = new List<int>();


            // check if the settings value is valid
            if (this.Settings.ItemPHColumns < 1)
                throw new Exception("The settings value for '" + nameof(this.Settings.ItemPHColumns) + "' is smaler than 1 and this is not allowed. So it is not possible to calculate the width of the '" + nameof(PVItemPlaceHolder) + "'.");


            // find the biggest width for each column
            for (int i = 0; i < this.Settings.ItemPHColumns; i++)
            {
                getBiggestColumnWidth(i);
            }


            // return the sum of all column width
            return this.ColumnWidths.Sum();
        }

        /// <summary>
        /// calculate the height of this item place holder and save all row heights in <see cref="m_heightPerRow"/>.
        /// </summary>
        /// <returns></returns>
        private int calculateHeight()
        {
            // reset the list which contains the calculated heights per row
            this.RowHeights = new List<int>();


            // check if the settings value is valid
            if (this.Settings.ItemPHColumns < 1)
                throw new Exception("The settings value for '" + nameof(this.Settings.ItemPHRows) + "' is smaler than 1 and this is not allowed. So it is not possible to calculate the height of the '" + nameof(PVItemPlaceHolder) + "'.");


            // find the biggest height for each row
            for (int i = 0; i < this.Settings.ItemPHColumns; i++)
            {
                getBiggestRowHeight(i);
            }


            // return the sum of all row heights
            return this.RowHeights.Sum();
        }

        /// <summary>
        /// find the biggest column width value of one column
        /// </summary>
        /// <param name="columnIndex">check the column with this index</param>
        /// <returns></returns>
        private int getBiggestColumnWidth(int columnIndex)
        {
            int biggestWidth = 0;

            // etherate through all items which are located in this column
            foreach (var item in this.Items.Where(x => x.Column == columnIndex))
            {
                // calculate the total width of the current item
                int curWidth = item.Width + item.Margin.Left + item.Margin.Right;

                // override the biggest width value if the current width is grather
                if (curWidth > biggestWidth)
                    biggestWidth = curWidth;
            }

            // add the new width to the column width list and return the value
            this.ColumnWidths.Add(biggestWidth);
            return biggestWidth;
        }

        /// <summary>
        /// find the biggest row height value of one row
        /// </summary>
        /// <param name="rowIndex">check the row with this index</param>
        /// <returns></returns>
        private int getBiggestRowHeight(int rowIndex)
        {
            int biggestHeight = 0;

            // etherate through all items which are located in this row
            foreach (var item in this.Items.Where(x => x.Row == rowIndex))
            {
                // calculate the total height of the current item
                int curheight = item.Width + item.Margin.Top + item.Margin.Bottom;

                // override the biggest height value if the current height is grather
                if (curheight > biggestHeight)
                    biggestHeight = curheight;
            }

            // add the new height to the row height list and return the value
            this.RowHeights.Add(biggestHeight);
            return biggestHeight;
        }



        #endregion

    }
}
