using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces.ProductionViewer
{
    /// <summary>
    /// This interface provides the values which are referenced in the view model for the production view labels next to its image
    /// </summary>
    public interface IPVLabel : IPVBaseNode
    {

        /// <summary>
        /// Shown Text
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Size of the text
        /// </summary>
        int FontSize { get; }
        
        /// <summary>
        /// Font familiy
        /// </summary>
        string FontFamily { get; }

        /// <summary>
        /// text color
        /// </summary>
        Color FontColor { get; }

        /// <summary>
        /// backgroundColor
        /// </summary>
        Color BackgroundColor { get; }

    }
}
