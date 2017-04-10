using System.Collections.Generic;

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
        List<FactorioItem> Items { get; }


        #endregion
        
        #region IO


        /// <summary>
        /// Read a file into the business layer
        /// </summary>
        /// <param name="path"></param>
        void ReadFile(string path);

        /// <summary>
        /// Write a file from the business layer
        /// </summary>
        /// <param name="path"></param>
        void WriteFile(string path);


        #endregion

        #region Public Methods

        

        #endregion

    }
}
