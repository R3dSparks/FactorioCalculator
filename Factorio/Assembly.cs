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


        public string PrintProduction(int quantity, int tabs = 0)
        {
            string output = "";

            for (int i = 0; i < tabs; i++)
                output += ("\t");

            output += ($"{this.AssemblyItem.Name}: {this.Quantity * quantity:F4} ({this.AssemblyItem.Productivity * this.Quantity * quantity:F4}/second)\n");

            if (this.SubAssembly.Count() != 0)
            {
                foreach (var subAssembly in this.SubAssembly)
                {
                    output += subAssembly.PrintProduction(quantity, tabs + 1);
                }
            }

            return output;
        }

        public Dictionary<FactorioItem, double> GetRaw(int quantity, params string[] args)
        {
            foreach (var itemName in args)
            {
                if (this.AssemblyItem.Name == itemName)
                {
                    Dictionary<FactorioItem, double> dict = new Dictionary<FactorioItem, double>();

                    dict.Add(this.AssemblyItem, this.AssemblyItem.Productivity * this.Quantity * quantity);

                    return dict;
                }
            }

            Dictionary<FactorioItem, double> itemProductivity = new Dictionary<FactorioItem, double>();

            foreach (var subAssembly in this.SubAssembly)
            {
                foreach (var subItemProductivity in subAssembly.GetRaw(quantity, args))
                {
                    if (itemProductivity.ContainsKey(subItemProductivity.Key))
                    {
                        itemProductivity[subItemProductivity.Key] += subItemProductivity.Value;
                    }
                    else
                    {
                        itemProductivity.Add(subItemProductivity.Key, subItemProductivity.Value);
                    }
                }
            }

            return itemProductivity;


        }
        #endregion

    }
}

