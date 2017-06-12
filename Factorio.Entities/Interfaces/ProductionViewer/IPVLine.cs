using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces.ProductionViewer
{
    /// <summary>
    /// This interface provides the values which are referenced in the view model for the production view lines
    /// </summary>
    public interface IPVLine : IPVBaseNode
    {

        /// <summary>
        /// The distance to the canves top for the first point
        /// </summary>
        int StartTop { get; }

        /// <summary>
        /// The diestance to the canvas left for the first point
        /// </summary>
        int StartLeft { get; }

        /// <summary>
        /// The distance to the canves top for the second point
        /// </summary>
        int EndTop { get; }

        /// <summary>
        /// The diestance to the canvas left for the second point
        /// </summary>
        int EndLeft { get; }

    }
}
