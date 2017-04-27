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
        void ReadFile();

        /// <summary>
        /// Write a file from the business layer
        /// </summary>
        void WriteFile();


        #endregion

        #region Public Methods

        

        #endregion

    }
}
