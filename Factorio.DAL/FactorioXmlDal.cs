using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Factorio.Entities;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Factorio.DAL
{
    /// <summary>
    /// This class saves <see cref="FactorioItem"/> into a xml file and reads from such a file.
    /// </summary>
    public class FactorioXmlDal : IFactorioXmlDal
    {

        #region Constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public FactorioXmlDal()
        {

        }

        #endregion

        #region Public Methods

        public ObservableCollection<FactorioItem> ReadItems(string path)
        {
            // Contains all items that are currently known
            var knownItems = new List<FactorioItem>();

            // Contains items that are found in recipes but aren't loaded yet
            var unknownItems = new List<FactorioItem>();            

            try
            {
                // Check if file exists, create one if it doesn't
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    FactorioXmlHelper.CreateXml(path);
                }

                XDocument itemsFile = XDocument.Load(path);

                foreach (var xmlItemData in itemsFile.Descendants(FactorioXmlHelper.XmlItemElement))
                {
                    FactorioItem newItem = FactorioXmlHelper.GetFactorioItemFromXmlData(xmlItemData, knownItems, unknownItems);

                    knownItems.Add(newItem);

                    newItem.TryAddRecipeData(xmlItemData, knownItems, unknownItems);
                }

                

            }
            catch (FactorioException)
            {
                // pass through
                throw;
            }
            catch (Exception ex)
            {
                // create a new exception to add the event code
                throw new FactorioException(DiagnosticEvents.DalXmlRead, "An error occurred while reading a xml file with the message: " + ex.Message, ex);
            }

            return new ObservableCollection<FactorioItem>(knownItems);

        }

        /// <summary>
        /// Save a list of <see cref="FactorioItem"/> in a file. Existing files will be overriden
        /// </summary>
        /// <param name="items">save these <see cref="FactorioItem"/>s</param>
        /// <param name="path">save the file here</param>
        public void SaveItems(ObservableCollection<FactorioItem> items, string path)
        {

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true
            };

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(FactorioXmlHelper.XmlMainElement);

            foreach (var item in items)
            {
                writer.WriteStartElement(FactorioXmlHelper.XmlItemElement);

                item.WriteXml(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();

        }

        #endregion

    }
}
