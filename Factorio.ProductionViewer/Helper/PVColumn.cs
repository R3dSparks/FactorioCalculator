using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer.Helper
{
    /// <summary>
    /// This class is used to get the biggest width out of all item place holder in a column.
    /// </summary>
    public class PVColumn : IRestable
    {

        #region Private Variables



        private PVColumn m_previousColumn;
        private List<PVItemPlaceHolder> m_itemHolder;
        private int m_index;
        private int m_width;



        #endregion

        #region Properties



        /// <summary>
        /// The column left to this column.
        /// If it is null, then this column is the first column.
        /// </summary>
        public PVColumn PreviousColumn
        {
            get { return m_previousColumn; }
            private set { m_previousColumn = value; }
        }

        /// <summary>
        /// All item placeholder which are located in this column.
        /// </summary>
        public List<PVItemPlaceHolder> ItemPlaceholder
        {
            get
            {
                if (m_itemHolder == null)
                    m_itemHolder = new List<PVItemPlaceHolder>();
                return m_itemHolder;
            }
            private set { m_itemHolder = value; }
        }

        /// <summary>
        /// Get the biggest calculated width which is needed for this column.
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
        /// Index of this column
        /// </summary>
        public int Index
        {
            get { return m_index; }
            private set { m_index = value; }
        }



        #endregion

        #region Constructor



        /// <summary>
        /// Default constructor. This creates a column
        /// </summary>
        public PVColumn() : this(null)
        {
            this.Index = 0;
        }

        /// <summary>
        /// Create a new column with a relation to the column on the left side.
        /// </summary>
        /// <param name="previousColumn">Row above this row</param>
        public PVColumn(PVColumn previousColumn)
        {
            this.PreviousColumn = previousColumn;
            this.Index = previousColumn.Index + 1;
        }



        #endregion

        #region Interface Methods



        /// <summary>
        /// Reset the <see cref="Width"/> value
        /// </summary>
        public void RestFixedValues()
        {
            this.Width = 0;
        }



        #endregion

        #region Public Methods



        /// <summary>
        /// Adds the item placeholder to the list and creates the relation between the classes.
        /// </summary>
        /// <param name="itemPlaceholder"></param>
        public void AddItemPlaceholder(PVItemPlaceHolder itemPlaceholder)
        {
            itemPlaceholder.ParentColumns.Add(this);
            this.ItemPlaceholder.Add(itemPlaceholder);
        }



        #endregion

        #region Private Methods



        /// <summary>
        /// calculate the width of this column
        /// </summary>
        /// <returns></returns>
        private int calculateWidth()
        {
            int maxWidth = 0;

            // etherate through all item placeholder which are located in this column
            foreach (var itemPh in this.ItemPlaceholder)
            {
                int curWidth = 0;

                // get the width value for this item placeholder
                // check if this placeholder is located in more than one column
                if (itemPh.ParentColumns.Count == 1)
                    // if not use the item placeholder width value
                    curWidth = itemPh.Width;
                else
                {
                    // get the total space which is availible by default
                    int totalSpace = itemPh.ParentColumns.Count * itemPh.Settings.WidthOffset +    // calculate the width between all images befor this image
                        itemPh.ParentColumns.Count * itemPh.Settings.ItemPHMinWidth;       // calculate the total image width of all images before this image

                    // check if the total space is bigger than the item placeholder width
                    if (totalSpace >= itemPh.Width)
                        // if it is bigger use the default value
                        curWidth = itemPh.Settings.ItemPHMinWidth;
                    else
                    {
                        // subtract the total space from the item placeholder and the spaces between the columns
                        int overlap = itemPh.Width - totalSpace - itemPh.ParentColumns.Count * itemPh.Settings.WidthOffset;

                        // divide the overlap by the amount of columns it uses
                        int partPerColumn = overlap / itemPh.ParentColumns.Count;

                        // add 1 if there remainder is not equal to 0
                        if (overlap % itemPh.ParentColumns.Count != 0)
                            partPerColumn++;

                        
                        curWidth = partPerColumn + itemPh.Settings.ItemPHMinWidth;
                    }
                }

                // if curWidth is bigger than maxWidth override the value
                if (curWidth > maxWidth)
                    maxWidth = curWidth;
            }
            return maxWidth;
        }



        #endregion

    }
}
