using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer.Helper
{
    /// <summary>
    /// This interface contains the basic information so that a item can be added to a <see cref="PVItemPlaceHolder"/>
    /// </summary>
    public interface IPVItem
    {
        /// <summary>
        /// Height of the item
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Width of the item
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Margin of the item
        /// </summary>
        Margins Margin { get; }

        /// <summary>
        /// Column position of this item.
        /// <para>
        /// The first coulmn is 0.
        /// </para>
        /// If a item already exists in that position value, this one gets ignored.
        /// If the <see cref="PVItemPlaceHolder"/> does not have a grid with this column number it also get ignored.
        /// </summary>
        int Column { get; }

        /// <summary>
        /// Row position of this item.
        /// <para>
        /// The first row is 0.
        /// </para>
        /// If a item already exists in that position value, this one gets ignored.
        /// If the <see cref="PVItemPlaceHolder"/> does not have a grid with this row number it also get ignored.
        /// </summary>
        int Row { get; }
        
        /// <summary>
        /// Reference to the placeholder where the item is located
        /// </summary>
        PVItemPlaceHolder ParentPlaceHolder { get; set; }
    }
}
