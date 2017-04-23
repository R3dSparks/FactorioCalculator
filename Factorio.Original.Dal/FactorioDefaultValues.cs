using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Factorio.Original.Entities;
using Factorio.Original.Enums;


namespace Factorio.Original.Dal
{
    /// <summary>
    /// contains the default values 
    /// </summary>
    public static class FactorioDefaultValues
    {
        /// <summary>
        /// default energy value if 
        /// </summary>
        public const double CraftingTime = 0.5;
        /// <summary>
        /// default item type
        /// </summary>
        public const FItemType ItemType = FItemType.Item;
        /// <summary>
        /// default category
        /// </summary>
        public static readonly FCategory Category = new FCategory("Crafting", FCraftingSourceType.AssemblingMachine);
    }
}
