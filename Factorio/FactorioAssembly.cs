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

        public FactorioAssembly TopAssembly { get; private set; }

        public FactorioItem AssemblyItem { get; private set; }

        public double Quantity { get; private set; }
        
        /// <summary>
        /// List of assemlies that are needed to run this assembly
        /// </summary>
        public List<FactorioAssembly> SubAssemblies
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
                UpdateSubAssemblyTree();
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
                    return FactorioHelper.DefaultCraftingSpeeds[AssemblyItem.DefaultCraftingType];
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
                SubAssemblies.Add(new FactorioAssembly(item.Key, this, item.Value));
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
            TopAssembly = topAssembly;

            AssemblyItem = assemblyItem;

            Quantity = (quantity * ((topAssembly.Quantity * topAssembly.AssemblyItem.Productivity)) / (assemblyItem.Productivity * topAssembly.AssemblyItem.CraftingOutput));

            Quantity *= topAssembly.CraftingSpeed / CraftingSpeed;

            if (assemblyItem.Recipe != null)
            {
                foreach (var item in assemblyItem.Recipe)
                {
                    SubAssemblies.Add(new FactorioAssembly(item.Key, this, item.Value));
                }
            }
        }

        #endregion

        #region Public Methods

        public void UpdateSubAssemblyTree()
        {
            Quantity = (TopAssembly.AssemblyItem.Recipe[AssemblyItem] * ((TopAssembly.Quantity * TopAssembly.AssemblyItem.Productivity)) / (AssemblyItem.Productivity * TopAssembly.AssemblyItem.CraftingOutput));

            Quantity *= TopAssembly.CraftingSpeed / CraftingSpeed;

            foreach (FactorioAssembly subAssembly in SubAssemblies)
            {
                subAssembly.UpdateSubAssemblyTree();
            }
        }

        #endregion

       

    }
}

