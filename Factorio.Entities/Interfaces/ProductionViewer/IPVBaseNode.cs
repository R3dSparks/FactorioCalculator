using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces.ProductionViewer
{
    /// <summary>
    /// This base node contains the basic information for all items which are shown in the canas.
    /// </summary>
    public interface IPVBaseNode
    {

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        int Left { get; }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        int Top { get; }

    }
}
