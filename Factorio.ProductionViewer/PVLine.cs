using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer 
{
    public class PVLine : IPVLine
    {
        
        #region Interface Properties



        /// <summary>
        /// The distance to the canves top for the first point
        /// </summary>
        public int StartTop { get; }

        /// <summary>
        /// The diestance to the canvas left for the first point
        /// </summary>
        public int StartLeft { get; }

        /// <summary>
        /// The distance to the canves top for the second point
        /// </summary>
        public int EndTop { get; }

        /// <summary>
        /// The diestance to the canvas left for the second point
        /// </summary>
        public int EndLeft { get; }



        #endregion

        #region Constructors



        /// <summary>
        /// defaul constructor
        /// </summary>
        public PVLine()
        {

        }



        #endregion

    }
}
