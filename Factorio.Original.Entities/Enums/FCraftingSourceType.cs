using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Original.Enums
{
    /// <summary>
    /// used to define where something is crafted
    /// </summary>
    public enum FCraftingSourceType
    {
        /// <summary>
        /// crafted in an assembling machine
        /// </summary>
        AssemblingMachine,
        /// <summary>
        /// crafted in a furnace
        /// </summary>
        Furnace,
        /// <summary>
        /// crafted in a chemical plant
        /// </summary>
        ChemicalPlant,
        /// <summary>
        /// crafted in a refinery
        /// </summary>
        Refinery
    }
}
