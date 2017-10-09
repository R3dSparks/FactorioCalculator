using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces.ProductionViewer
{
    public interface IPVFactorioItemContainer
    {

        int Width { get; }

        /// <summary>
        /// Height of the container
        /// </summary>
        int ContainerHeight { get; }

        /// <summary>
        /// Width of the container
        /// </summary>
        int ContainerWidth { get; }

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        int Left { get; }


        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        int Top { get; }

        /// <summary>
        /// Image for this item container
        /// </summary>
        IPVImage Image { get; }

    }
}
