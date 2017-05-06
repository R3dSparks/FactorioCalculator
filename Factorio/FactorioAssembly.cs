using System;
using System.Collections.Generic;
using System.Linq;

using Factorio.Entities;
using Factorio.DAL;
using Factorio.Entities.Enum;

namespace Factorio
{
    public class FactorioAssembly
    {

        #region Private Variables

        private List<FactorioAssembly> m_subAssembly;

        private CraftingStation? m_craftingStation;

        #endregion

        #region Public Properties

        public FactorioItem AssemblyItem { get; private set; }

        public double Quantity { get; private set; }
        
        /// <summary>
        /// List of assemlies that are needed to run this assembly
        /// </summary>
        public List<FactorioAssembly> SubAssembly
        {
            get
            {
                if (m_subAssembly == null)
                    m_subAssembly = new List<FactorioAssembly>();

                return m_subAssembly;
            }
        }

        /// <summary>
        /// The specified crafting station for this assembly
        /// </summary>
        public CraftingStation CraftingStation
        {
            get
            {
                return m_craftingStation ?? CraftingStation.AssemblingMachine1;
            }

            set
            {
                m_craftingStation = value;
            }
        }

        /// <summary>
        /// Crafting speed for this assembly. Get default item crafting speed if crafting station is not set.
        /// </summary>
        public double CraftingSpeed
        {
            get
            {
                if (m_craftingStation == null)
                    return FactorioHelper.DefaultCraftingSpeeds[AssemblyItem.DefaultCraftingStation];
                else
                    return FactorioHelper.CraftingSpeeds[CraftingStation];
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="assemblyItem"></param>
        public FactorioAssembly(FactorioItem assemblyItem)
        {
            AssemblyItem = assemblyItem;

            Quantity = 1;

            if (assemblyItem.Recipe == null)
                return;

            foreach (var item in assemblyItem.Recipe)
            {
                SubAssembly.Add(new FactorioAssembly(item.Key, this, item.Value));
            }
        }

        /// <summary>
        /// Constructor for subassemblies
        /// </summary>
        /// <param name="assemblyItem"></param>
        /// <param name="topAssembly"></param>
        /// <param name="quantity"></param>
        public FactorioAssembly(FactorioItem assemblyItem, FactorioAssembly topAssembly, int quantity)
        {
            AssemblyItem = assemblyItem;

            Quantity = (quantity * ((topAssembly.Quantity * topAssembly.AssemblyItem.Productivity)) / (assemblyItem.Productivity * topAssembly.AssemblyItem.CraftingOutput));

            Quantity *= topAssembly.CraftingSpeed / CraftingSpeed;

            if (assemblyItem.Recipe != null)
            {
                foreach (var item in assemblyItem.Recipe)
                {
                    SubAssembly.Add(new FactorioAssembly(item.Key, this, item.Value));
                }
            }
        }

        #endregion

        #region Public Methods



        #endregion

        //#region Public methods

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="quantity"></param>
        ///// <param name="tabs"></param>
        ///// <returns></returns>
        //public string GetProductionPerAssembly(double quantity, int tabs = 0)
        //{
        //    string output = "";

        //    for (int i = 0; i < tabs; i++)
        //        output += ("\t");

        //    output += ($"{this.AssemblyItem.Name}: {this.Quantity * quantity:F4} ({this.AssemblyItem.Productivity * this.Quantity * quantity * FactorioHelper.CraftingSpeeds[this.CraftingStation]:F4}/second)\n");

        //    if (this.SubAssembly.Count() != 0)
        //    {
        //        foreach (var subAssembly in this.SubAssembly)
        //        {
        //            output += subAssembly.GetProductionPerAssembly(quantity, tabs + 1);
        //        }
        //    }

        //    return output;
        //}

        //public string GetProductionPerSecond(double itemsPerSecond)
        //{
        //    return GetProductionPerAssembly(itemsPerSecond / this.AssemblyItem.Productivity);
        //}


        ///// <summary>
        ///// Get all items and needed productivity that are needed for this production
        ///// </summary>
        ///// <param name="quantity">Quantity of assembling machines</param>
        ///// <param name="rawItems">Dictionary where all items and there productivity will be saved</param>
        //public string GetItemSummary(double quantity)
        //{
        //    string output = "";

        //    Dictionary<FactorioItem, double> rawItems = new Dictionary<FactorioItem, double>();

        //    getItemSummary(quantity, rawItems);

        //    foreach(var item in rawItems)
        //    {
        //        output += $"{item.Key.Name}: {item.Value}x{item.Key.DefaultCraftingStation.ToString()} | {item.Value * item.Key.Productivity * FactorioHelper.CraftingSpeeds[item.Key.DefaultCraftingStation]}/second\n";
        //    }

        //    return output;

        //}

        //private void getItemSummary(double quantity, Dictionary<FactorioItem, double> rawItems)
        //{
        //    if (rawItems.ContainsKey(this.AssemblyItem) == false)
        //        rawItems.Add(this.AssemblyItem, this.Quantity * quantity);
        //    else
        //        rawItems[this.AssemblyItem] += this.Quantity * quantity;


        //    foreach (var assembly in this.SubAssembly)
        //    {
        //        assembly.getItemSummary(quantity, rawItems);
        //    }
        //}

        //#endregion

    }
}

