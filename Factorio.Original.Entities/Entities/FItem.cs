using Factorio.Original.Enums;

namespace Factorio.Original.Entities
{
    public class FItem
    {

        #region Properties


        /// <summary>
        /// item name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// path to its icon
        /// </summary>
        public string IconPath { get; set; }
        /// <summary>
        /// type of the item
        /// </summary>
        public FItemType Type { get; set; }
        /// <summary>
        /// category of the item
        /// </summary>
        public FCategory Category { get; set; }
        


        #endregion



        public FItem()
        {

        }
    }
}
