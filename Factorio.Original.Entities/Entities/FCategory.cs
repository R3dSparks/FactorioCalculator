using Factorio.Original.Enums;

namespace Factorio.Original.Entities
{
    public class FCategory
    {

        #region Properties


        /// <summary>
        /// name of the category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// where this category is crafted in
        /// </summary>
        public FCraftingSourceType CraftingSource { get; set; }



        #endregion


        /// <summary>
        /// defaut category
        /// </summary>
        public FCategory()
        {

        }


        public FCategory(string name, FCraftingSourceType type)
        {
            this.Name = name;
            this.CraftingSource = FCraftingSourceType.AssemblingMachine;
        }
    }
}
