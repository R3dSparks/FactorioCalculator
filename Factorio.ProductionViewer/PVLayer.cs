using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    class PVLayer
    {

        #region Private Variables





        #endregion

        #region Properties



        /// <summary>
        /// Level of this layer.
        /// The first level is 0
        /// </summary>
        public int Level { get; set; }



        #endregion

        #region Constructor



        /// <summary>
        /// create a new layer and define its level
        /// </summary>
        /// <param name="level"></param>
        public PVLayer(int level)
        {
            this.Level = level;
        }

        #endregion

    }
}
