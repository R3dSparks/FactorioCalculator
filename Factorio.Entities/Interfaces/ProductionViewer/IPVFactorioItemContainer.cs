using Factorio.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces.ProductionViewer
{
    public interface IPVFactorioItemContainer
    {
        /// <summary>
        /// List of crafting stations for the CraftingStation ComboBox
        /// </summary>
        List<CraftingStation> AssemblyOptions { get; }

        /// <summary>
        /// Width of the container with all following containers and offsets
        /// </summary>
        int SubTreeWidth { get; }

        IFactorioAssembly Assembly { get;}

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

        /// <summary>
        /// Crafting station quantity for an FactorioItem
        /// </summary>
        double Quantity { get; set; }

    }
}
