using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces.ProductionViewer
{
    /// <summary>
    /// This interface prvides the values which are referenced in the view model for the production view images
    /// </summary>
    public interface IPVImage
    {

        /// <summary>
        /// Width of the image
        /// </summary>
        int ImageWidth { get; }

        /// <summary>
        /// Height of the image
        /// </summary>
        int ImageHeight { get; }

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        int Left { get; }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        int Top { get; }
        
        /// <summary>
        /// Path to the image
        /// </summary>
        string ImagePath { get; }



    }
}
