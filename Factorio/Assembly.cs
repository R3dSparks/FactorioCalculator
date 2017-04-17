using System;
using System.Collections.Generic;
using System.Linq;

using Factorio.Entities;
using Factorio.DAL;

namespace Factorio
{
    public class Assembly
    {


        #region Properties


        public FactorioItem AssemblyItem { get; private set; }

        public double Quantity { get; private set; }
        // Tip: für listen immer eine private variable anlegen und im falle wenn diese null ist, eine neue leere liste anzulegen.
        public List<Assembly> SubAssembly { get; set; }

        public Crafting CraftingStation { get; set; }

        public double CraftingSpeed { get; set; }

        #endregion

        #region Constructors


        public Assembly(FactorioItem assemblyItem)
        {
            AssemblyItem = assemblyItem;
          
            Quantity = 1;

            if (assemblyItem.Recipe == null)
                return;

            SubAssembly = new List<Assembly>();

            foreach (var item in assemblyItem.Recipe)
            {
                SubAssembly.Add(new Assembly(item.Key, this, item.Value));
            }
        }



        public Assembly(FactorioItem assemblyItem, Assembly topAssembly, int quantity)
        {
            AssemblyItem = assemblyItem;

            Quantity = (quantity * ((topAssembly.Quantity * topAssembly.AssemblyItem.Productivity)) / (assemblyItem.Productivity * topAssembly.AssemblyItem.CraftingOutput));

            SubAssembly = new List<Assembly>();

            if (assemblyItem.Recipe != null)
            {
                foreach (var item in assemblyItem.Recipe)
                {
                    SubAssembly.Add(new Assembly(item.Key, this, item.Value));
                }
            }
        }



        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="tabs"></param>
        /// <returns></returns>
        public string GetProductionPerAssembly(double quantity, int tabs = 0)
        {
            string output = "";

            for (int i = 0; i < tabs; i++)
                output += ("\t");

            output += ($"{this.AssemblyItem.Name}: {this.Quantity * quantity:F4} ({this.AssemblyItem.Productivity * this.Quantity * quantity:F4}/second)\n");

            if (this.SubAssembly.Count() != 0)
            {
                foreach (var subAssembly in this.SubAssembly)
                {
                    output += subAssembly.GetProductionPerAssembly(quantity, tabs + 1);
                }
            }

            return output;
        }

        public string GetProductionPerSecond(double itemsPerSecond)
        {
            return GetProductionPerAssembly(itemsPerSecond / this.AssemblyItem.Productivity);
        }


        /// <summary>
        /// Get all items and needed productivity that are needed for this production
        /// </summary>
        /// <param name="quantity">Quantity of assembling machines</param>
        /// <param name="rawItems">Dictionary where all items and there productivity will be saved</param>
        public void GetItemSummary(double quantity, Dictionary<FactorioItem, double> rawItems)
        {
            if (!rawItems.ContainsKey(this.AssemblyItem))
                rawItems.Add(this.AssemblyItem, this.Quantity * quantity);
            else
                rawItems[this.AssemblyItem] += this.Quantity * quantity;
            

            foreach (var assembly in this.SubAssembly)
            {
                assembly.GetItemSummary(quantity, rawItems);
            }

        }

        #endregion

    }
}

