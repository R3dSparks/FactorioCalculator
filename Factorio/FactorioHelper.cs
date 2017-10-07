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
            {CraftingType.Drill, 1 },
        };

        /// <summary>
        /// Crafting speeds for all crafting stations
        /// </summary>
        public static readonly Dictionary<CraftingStation, double> CraftingSpeeds = new Dictionary<CraftingStation, double>()
        {
            {CraftingStation.AssemblingMachine1 , 0.5},
            {CraftingStation.AssemblingMachine2 , 0.75},
            {CraftingStation.AssemblingMachine3 , 1.25},
            {CraftingStation.ChemicalPlant , 1.25},
            {CraftingStation.ElectricFurnace , 2},
            {CraftingStation.Refinary , 1},
            {CraftingStation.SteelFurnace , 2},
            {CraftingStation.StoneFurnace , 1},
        };

        public static List<CraftingStation> GetCraftingStationsFromCraftingType(CraftingType type)
        {
            List<CraftingStation> stations = new List<CraftingStation>();

            switch (type)
            {
                case CraftingType.AssemblingMachine:
                    stations.Add(CraftingStation.AssemblingMachine1);
                    stations.Add(CraftingStation.AssemblingMachine2);
                    stations.Add(CraftingStation.AssemblingMachine3);
                    break;
                case CraftingType.Furnace:
                    stations.Add(CraftingStation.StoneFurnace);
                    stations.Add(CraftingStation.SteelFurnace);
                    stations.Add(CraftingStation.ElectricFurnace);
                    break;
                case CraftingType.ChemicalPlant:
                    stations.Add(CraftingStation.ChemicalPlant);
                    break;
                case CraftingType.Refinery:
                    stations.Add(CraftingStation.Refinary);
                    break;
                case CraftingType.Drill:
                    break;
                default:
                    break;
            }

            return stations;
        }

    }
}
