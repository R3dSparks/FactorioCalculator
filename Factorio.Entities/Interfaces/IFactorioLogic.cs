using Factorio.Entities.Interfaces;
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

        IFactorioSettings Settings { get; set; }

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
        /// Remove an item from a recipe
        /// </summary>
        /// <param name="item"></param>
        /// <param name="recipeItem"></param>
        void RemoveRecipe(FactorioItem item, FactorioItem recipeItem);

        /// <summary>
        /// Add an item to a recipe
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quanity"></param>
        /// <param name="recipeItem"></param>
        void AddRecipe(FactorioItem item, int quanity, FactorioItem recipeItem);

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
        void AddItem(string name, int output, double time, CraftingType crafting, string path);

        /// <summary>
        /// Remove an item from Items
        /// </summary>
        /// <param name="item"></param>
        void RemoveItem(FactorioItem item);

        #endregion

    }
}
