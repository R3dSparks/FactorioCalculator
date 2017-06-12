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



        private List<PVImage> m_images;



        #endregion

        #region Properties



        /// <summary>
        /// Level of this layer.
        /// The first level is 0
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Images which are located in this layer
        /// </summary>
        public List<PVImage> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<PVImage>();
                return m_images;
            }
        }

        /// <summary>
        /// Public reference to its parent tree structure
        /// </summary>
        public PVTreeStructure ParentTreeStructure { get; private set; }



        #endregion

        #region Constructor



        /// <summary>
        /// create a new layer and define its level
        /// </summary>
        /// <param name="level">level of this layer</param>
        /// <param name="treeStructure">parent tree structure</param>
        public PVLayer(PVTreeStructure treeStructure, int level)
        {
            this.Level = level;
            this.ParentTreeStructure = treeStructure;
        }

        #endregion
        

    }
}
