using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer.Helper
{
    /// <summary>
    /// This class is used to get the biggest hight out of all item place holder in a row.
    /// </summary>
    public class PVRow : IRestable
    {

        #region Private Variables

        

        private PVRow m_previousRow;
        private List<PVItemPlaceHolder> m_itemHolder;
        private int m_index;
        private int m_height;



        #endregion

        #region Properties



        /// <summary>
        /// The row above this row.
        /// If it is null, then this row is the top row.
        /// </summary>
        public PVRow PreviousRow
        {
            get { return m_previousRow; }
            private set { m_previousRow = value; }
        }

        /// <summary>
        /// All item placeholder which are located in this row.
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
        /// Get the biggest height of all item placeholder.
        /// </summary>
        public int Height
        {
            get
            {
                if(m_height == 0)
                    m_height = this.ItemPlaceholder.Max(x => x.Height);
                return m_height;
            }
            private set { m_height = value; }
        }

        /// <summary>
        /// Index of this row
        /// </summary>
        public int Index
        {
            get { return m_index; }
            private set { m_index = value; }
        }



        #endregion

        #region Constructor



        /// <summary>
        /// Default constructor. This creates a top row
        /// </summary>
        public PVRow() : this(null)
        {
            this.Index = 0;
        }

        /// <summary>
        /// Create a new row with a relation to the row above.
        /// </summary>
        /// <param name="previousRow">Row above this row</param>
        public PVRow(PVRow previousRow)
        {
            this.PreviousRow = previousRow;
            this.Index = previousRow.Index + 1;
        }



        #endregion

        #region Interface Methods



        /// <summary>
        /// reset the <see cref="Height"/> value
        /// </summary>
        public void RestFixedValues()
        {
            this.Height = 0;
        }



        #endregion

        #region Public methods



        /// <summary>
        /// Adds the item placeholder to the list and creates the relation between the classes.
        /// </summary>
        /// <param name="itemPlaceholder"></param>
        public void AddItemPlaceholder(PVItemPlaceHolder itemPlaceholder)
        {
            itemPlaceholder.ParentRow = this;
            this.ItemPlaceholder.Add(itemPlaceholder);
        }



        #endregion

    }
}
