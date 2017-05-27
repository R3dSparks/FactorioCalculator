using Factorio.Entities;
using Factorio.Entities.Enum;
using System.Collections.Generic;

namespace Factorio
{
    public static class FactorioHelper
    {
        /// <summary>
        /// Default crafting speeds for the different crafting station types
        /// </summary>
        public static readonly Dictionary<CraftingType, double> DefaultCraftingSpeeds = new Dictionary<CraftingType, double>()
        {
            {CraftingType.AssemblingMachine, 1},
            {CraftingType.ChemicalPlant, 1 },
            {CraftingType.Furnace, 1 },
            {CraftingType.Refinery, 1 },
        };

        /// <summary>
        /// Crafting speeds for all crafting stations
        /// </summary>
        public static readonly Dictionary<CraftingStation, double> CraftingSpeeds = new Dictionary<CraftingStation, double>()
        {
            {CraftingStation.AssemblingMachine1 , 1},
            {CraftingStation.AssemblingMachine2 , 1},
            {CraftingStation.AssemblingMachine3 , 1},
            {CraftingStation.ChemicalPlant , 1},
            {CraftingStation.ElectricFurnace , 1},
            {CraftingStation.Refinary , 1},
            {CraftingStation.SteelFurnace , 1},
            {CraftingStation.StoneFurnace , 1},
        };

    }
}
