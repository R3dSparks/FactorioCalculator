using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Factorio.Entities
{
    /// <summary>
    /// This object represents one item in factorio
    /// </summary>
    public class FactorioItem
    {

        #region Properties

        /// <summary>
        /// name of the item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Profuctivity means items per second
        /// </summary>
        public double Productivity { get; set; }
        /// <summary>
        /// The amount which is crafted with one craft
        /// </summary>
        public int CraftingOutput { get; set; }
        /// <summary>
        /// How long it takes to craft it
        /// </summary>
        public double CraftingTime { get; set; }
        /// <summary>
        /// Which <see cref="FactorioItem"/> are needed to craft this item
        /// </summary>
        public Dictionary<FactorioItem, int> Recipe { get; private set; }
        /// <summary>
        /// Where this item is crafted
        /// </summary>
        public Crafting MadeIn { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// default constructor
        /// </summary>
        public FactorioItem()
        {

        }
        /// <summary>
        /// create a <see cref="FactorioItem"/> with a name, craft amount and a crafting time
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="time"></param>
        public FactorioItem(string name, int output, double time)
        {
            Name = name;
            CraftingOutput = output;
            CraftingTime = time;
            Productivity = output / time;
        }

        #endregion
        
        #region Public methods


        /// <summary>
        /// add a recipe item which is needed to craft this item
        /// </summary>
        /// <param name="item">the referenc to the item</param>
        /// <param name="quantity">the amount which is needed for the craft</param>
        public void AddRecipeItem(FactorioItem item, int quantity)
        {
            if (Recipe == null)
                Recipe = new Dictionary<FactorioItem, int>();

            if (!Recipe.ContainsKey(item))
            {
                Recipe.Add(item, quantity);
            }

        }
        #endregion

    }
}
