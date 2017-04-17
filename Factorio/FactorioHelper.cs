using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio
{
    public static class FactorioHelper
    {
        /// <summary>
        /// Contains the crafting speeds for <see cref="Crafting"/>
        /// </summary>
        public static readonly Dictionary<Crafting, double> CraftingSpeeds = new Dictionary<Crafting, double>
        {
            {Crafting.AssemblingMachine1, 0.5 },
            {Crafting.AssemblingMachine2, 0.75 },
            {Crafting.AssemblingMachine3, 1.25 },
            {Crafting.StoneFurnace, 1 },
            {Crafting.SteelFurnace, 2 },
            {Crafting.ElectricFurnace, 2 },
            {Crafting.ChemicalPlant, 1.25 },
            {Crafting.Refinery, 1 }

        };

    }
}
