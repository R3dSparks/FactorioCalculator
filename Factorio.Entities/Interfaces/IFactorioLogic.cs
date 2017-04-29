using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Factorio.Entities
{
    /// <summary>
    /// This interface is used for the communication between the business layer and the presentation layer
    /// </summary>
    public interface IFactorioLogic
    {
        #region Properties


        /// <summary>
        /// contains all items from the business layer
        /// </summary>
        ObservableCollection<FactorioItem> Items { get; }


        #endregion
        
        #region IO


        /// <summary>
        /// Read a file into the business layer
        /// </summary>
        void LoadItems();

        /// <summary>
        /// Write a file from the business layer
        /// </summary>
        void SaveItems();


        #endregion

        #region Public Methods

        /// <summary>
        /// Add an item to Items from raw input
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <exception cref="FactorioException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        void AddItem(string arg1, int arg2, double arg3, Crafting arg4);

        /// <summary>
        /// Remove an item from Items
        /// </summary>
        /// <param name="item"></param>
        void RemoveItem(FactorioItem item);

        #endregion

    }
}
