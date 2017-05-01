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
        /// Get the id of an item
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetItemId(string name);

        /// <summary>
        /// Add an item to Items from raw input
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <exception cref="FactorioException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        void AddItem(string name, int output, double time, Crafting crafting, string path);

        /// <summary>
        /// Edit an existing item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="time"></param>
        /// <param name="crafting"></param>
        /// <param name="path"></param>
        void EditItem(int id, string name, int output, double time, Crafting crafting, string path);

        /// <summary>
        /// Remove an item from Items
        /// </summary>
        /// <param name="item"></param>
        void RemoveItem(FactorioItem item);

        #endregion

    }
}
