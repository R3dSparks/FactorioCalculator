using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    /// <summary>
    /// This enum is used to define the location of the label to its related image.
    /// </summary>
    public enum PVLabelLocation
    {
        /// <summary>
        /// The label is above the image
        /// </summary>
        North,
        /// <summary>
        /// The label is on the right side of the image
        /// </summary>
        East,
        /// <summary>
        /// The label is below the image
        /// </summary>
        Sourth,
        /// <summary>
        /// The label is on the left side of the image
        /// </summary>
        West
    }
}
