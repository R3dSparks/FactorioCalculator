using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities
{
    /// <summary>
    /// This interface is used for the communication between the business layer and the data access layer (DAL)
    /// </summary>
    public interface IFactorioXmlDal
    {
        /// <summary>
        /// Read all <see cref="FactorioItem"/> from a xml file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        List<FactorioItem> ReadItems(string path);


        /// <summary>
        /// Save a list of <see cref="FactorioItem"/> in a file. Existing files will be overriden
        /// </summary>
        /// <param name="items">save these <see cref="FactorioItem"/>s</param>
        /// <param name="path">save the file here</param>
        void SaveItems(List<FactorioItem> items, string path);

    }
}
