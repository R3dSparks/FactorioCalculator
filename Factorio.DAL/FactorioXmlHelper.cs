using System.Xml;
using Factorio.Entities;
using System;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Factorio.DAL
{
    /// <summary>
    /// This class extends the <see cref="FactorioItem"/> class with the needed functionality to save it.
    /// </summary>
    static class FactorioXmlHelper
    {

        #region XML IO Methods

        

        

        #endregion


        /// <summary>
        /// Check if an image file exist at the given path.
        /// </summary>
        /// <param name="path">check this path</param>
        /// <returns>true if the file exists, false if not</returns>
        public static bool IsImagePathValid(string path)
        {
            if (String.IsNullOrEmpty(path))
                return false;

            return File.Exists(path);
        }

        /// <summary>
        /// Create a standart xml file
        /// </summary>
        /// <param name="path"></param>
        public static void CreateXml(string path, string xmlMainElement)
        {
            XDocument doc = new XDocument();

            doc.Add(new XElement(xmlMainElement));

            doc.Save(path);
        }
    }
}
