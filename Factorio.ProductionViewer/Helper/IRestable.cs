using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer.Helper
{
    /// <summary>
    /// This interface is used from the <see cref="PVTreeStructure"/> to reset all calculated values which are stored
    /// </summary>
    internal interface IRestable
    {

        /// <summary>
        /// Reset all stored calculated values
        /// </summary>
        void RestFixedValues();

    }
}
