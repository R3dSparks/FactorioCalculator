using System;
using System.Collections.Generic;
using System.Linq;

using Factorio.Entities;
using Factorio.DAL;
using Factorio.Entities.Enum;
using System.ComponentModel;

namespace Factorio
{
    public class FactorioAssembly : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

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
                changedCraftingStation();
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

        /// <summary>
        /// Is called whenever the CraftingStation Property is changed
        /// </summary>
        /// <param name="craftingStation"></param>
        private void changedCraftingStation()
        {
            SubAssembly.Clear();

            if (AssemblyItem.Recipe == null)
                return;

            foreach (var item in AssemblyItem.Recipe)
            {
                SubAssembly.Add(new FactorioAssembly(item.Key, this, item.Value));
            }
        }

        #endregion

    }
}

