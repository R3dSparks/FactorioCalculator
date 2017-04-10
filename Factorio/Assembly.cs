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
        public List<Assembly> SubAssembly { get; set; }
        #endregion

        #region Constructors
        public Assembly(FactorioItem assemblyItem)
        {
            AssemblyItem = assemblyItem;

            Quantity = 1;

            SubAssembly = new List<Assembly>();

            foreach(var item in assemblyItem.Recipe)
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
        public void Print(int quantity, int tabs = 0)
        {
            for (int i = 0; i < tabs; i++)
                Console.Write("\t");

            Console.WriteLine($"{this.AssemblyItem.Name}: {this.Quantity * quantity:F4} ({this.AssemblyItem.Productivity * this.Quantity * quantity:F4}/second)");
            if(this.SubAssembly.Count() != 0)
            {
                foreach (var subAssembly in this.SubAssembly)
                {
                    subAssembly.Print(quantity, tabs + 1);
                }
            }
            
        }

        public void PrintRaw(params string[] args)
        {

        }
        #endregion

    }
}
