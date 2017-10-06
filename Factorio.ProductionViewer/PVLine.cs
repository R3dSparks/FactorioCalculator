using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.ProductionViewer
{
    /// <summary>
    /// this class represents one line in the production viewer
    /// </summary>
    public class PVLine : IPVLine
    {

        #region Private Variables



        private PVFactorioItemContainer m_from;
        private PVFactorioItemContainer m_to;



        #endregion
        
        #region Interface Properties



        /// <summary>
        /// The distance to the canves top for the first point
        /// </summary>
        public int StartTop
        {
            // add the image top value with its height
            get { return m_from.Top + m_from.ContainerHeight; }
        }

        /// <summary>
        /// The diestance to the canvas left for the first point
        /// </summary>
        public int StartLeft
        {
            // add the image left value with half of its width
            get { return m_from.Left + (m_from.ContainerWidth / 2); }
        }

        /// <summary>
        /// The distance to the canves top for the second point
        /// </summary>
        public int EndTop
        {
            get { return m_to.Top; }
        }

        /// <summary>
        /// The diestance to the canvas left for the second point
        /// </summary>
        public int EndLeft
        {
            // add the image left value with half of its width
            get { return m_to.Left + (m_to.ContainerWidth / 2); }
        }



        #endregion

        #region Constructors



        /// <summary>
        /// Creates a line from a image in a higher level to a lower level image.
        /// </summary>
        /// <param name="from">the line starts at the bottom middle of this image</param>
        /// <param name="to">the line stop at the top middle of this image</param>
        public PVLine(PVFactorioItemContainer from, PVFactorioItemContainer to)
        {
            m_from = from;
            m_to = to;
        }



        #endregion

    }
}
