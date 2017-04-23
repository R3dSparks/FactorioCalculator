using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Original.Entities
{
    public class FRecipe
    {
        
        #region Properties

        
        /// <summary>
        /// name of the recipe
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// which ingredients are needed to craft it
        /// </summary>
        public Dictionary<FItem, int> Ingredients { get; set; }
        /// <summary>
        /// result of this recipe
        /// </summary>
        public FItem Result { get; set; }
        /// <summary>
        /// time it takes to carft the item
        /// </summary>
        public double CraftingTime { get; set; }


        #endregion


        /// <summary>
        /// default constructor
        /// </summary>
        public FRecipe()
        {

        }

    }
}
