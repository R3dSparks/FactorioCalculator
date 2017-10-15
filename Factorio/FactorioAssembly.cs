using System;
using System.Collections.Generic;
using System.Linq;

using Factorio.Entities;
using Factorio.DAL;
using Factorio.Entities.Enum;
using System.ComponentModel;
using Factorio.Entities.Interfaces;

namespace Factorio
{
    public class FactorioAssembly : INotifyPropertyChanged, IFactorioAssembly
    {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Private Variables

        private List<IFactorioAssembly> m_subAssembly;

        private CraftingStation? m_craftingStation;

        private IFactorioAssembly m_topAssembly; 

        private double m_quantity;

        #endregion

        #region Public Properties

        public FactorioItem AssemblyItem { get; private set; }

        /// <summary>
        /// Number of crafting stations needed
        /// </summary>
        public double Quantity
        {
            get
            {
                return m_quantity;
            }
            set
            {
                m_quantity = value;

                // Only update if this is the top most assembly
                if (m_topAssembly == null)
                {
                    UpdateAssembly();
                }

            }
        }

        public int ItemQuantity { get; set; }

        /// <summary>
        /// List of assemlies that are needed to run this assembly
        /// </summary>
        public List<IFactorioAssembly> SubAssembly
        {
            get
            {
                if (m_subAssembly == null)
                    m_subAssembly = new List<IFactorioAssembly>();

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
                return m_craftingStation ?? FactorioHelper.DefaultCraftingStation[AssemblyItem.DefaultCraftingType];
            }

            set
            {
                m_craftingStation = value;
                UpdateAssembly();
            }
        }

        /// <summary>
        /// Crafting speed for this assembly.
        /// </summary>
        public double CraftingSpeed
        {
            get
            {
                return FactorioHelper.CraftingSpeeds[CraftingStation];
            }
        }

        public Dictionary<FactorioItem, double> Summary
        {
            get
            {
                return GetSummary();
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

            ItemQuantity = quantity;

            m_topAssembly = topAssembly;

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

        /// <summary>
        /// Get a list of which items and how many crafting stations are needed
        /// </summary>
        /// <param name="summary"></param>
        /// <returns></returns>
        public Dictionary<FactorioItem, double> GetSummary(Dictionary<FactorioItem, double> summary = null)
        {
            if(summary == null)
                summary = new Dictionary<FactorioItem, double>();

            if (summary.ContainsKey(this.AssemblyItem) == false)
            {
                summary.Add(this.AssemblyItem, this.Quantity);
            }
            else
            {
                summary[this.AssemblyItem] += this.Quantity;
            }

            foreach (var assembly in this.SubAssembly)
            {
                assembly.GetSummary(summary);
            }

            return summary;
        }

        #endregion

        #region Private Methods

        public void UpdateAssembly()
        {
            if (m_topAssembly != null)
            {
                Quantity = (ItemQuantity * ((m_topAssembly.Quantity * m_topAssembly.AssemblyItem.Productivity)) / (this.AssemblyItem.Productivity * m_topAssembly.AssemblyItem.CraftingOutput));

                Quantity *= m_topAssembly.CraftingSpeed / CraftingSpeed;
            }

            foreach (var subAssembly in this.SubAssembly)
            {
                subAssembly.UpdateAssembly();
            }

        }

        #endregion

    }
}

