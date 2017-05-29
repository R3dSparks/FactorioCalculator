using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Factorio.Entities;
using System.Linq;
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

            // Contains all items that are known but have items in their recipe that are unkown
            var knownItemsWithoutRecipe = new List<FactorioItem>();

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

            }
            catch (Exception)
            {
                throw;
            }

            return new ObservableCollection<FactorioItem>(knownItems);

        }

        /// <summary>
        /// Read all <see cref="FactorioItem"/> from a xml file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        /// <exception cref="FactorioException"></exception>
        //public ObservableCollection<FactorioItem> ReadItems(string path)
        //{
        //    // contains items where all properties are known
        //    var knownItems = new List<FactorioItem>();
        //    // contains items where only the name is known
        //    var unknownItems = new List<FactorioItem>();
        //    XmlReader reader = null;

        //    try
        //    {
        //        // check if file exists
        //        if (!File.Exists(path))
        //        {
        //            Directory.CreateDirectory(Path.GetDirectoryName(path));
        //            FactorioXmlHelper.CreateXml(path);
        //        }

        //        reader = XmlReader.Create(path);
        //        FactorioItem currentItem = null;

        //        // read all lines
        //        while (reader.Read())
        //        {
        //            if (reader.Name == FactorioXmlHelper.XmlItemElement && reader.NodeType != XmlNodeType.EndElement)
        //            {
        //                this.readItemElement(reader, out currentItem, knownItems, unknownItems);
        //            }
        //            else if (reader.Name == FactorioXmlHelper.XmlCraftingElement)
        //            {
        //                this.readCraftingElement(reader, currentItem, knownItems, unknownItems);
        //            }
        //        }
        //    }
        //    catch (FactorioException)
        //    {
        //        // pass through
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        // create a new exception to add the event code
        //        throw new FactorioException(DiagnosticEvents.DalXmlRead, "An error occurred while reading a xml file with the message: " + ex.Message, ex);
        //    }
        //    finally
        //    {
        //        // make sure to close the reader
        //        if (reader != null)
        //            reader.Close();
        //    }

        //    return new ObservableCollection<FactorioItem>(knownItems);
        //}


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

        #region Private Methods

        /// <summary>
        /// create the file 
        /// </summary>
        /// <param name="path"></param>
        private void createXmlFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(FactorioXmlHelper.XmlMainElement);
            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Close();
        }

        /// <summary>
        /// read an item
        /// </summary>
        /// <param name="reader">xml reader with the item element</param>
        /// <param name="currentItem">get the reference of the currently read item</param>
        /// <param name="knownItems">all known items</param>
        /// <param name="unknownItems">all unknown items</param>
        private void readItemElement(XmlReader reader, out FactorioItem currentItem, List<FactorioItem> knownItems, List<FactorioItem> unknownItems)
        {
            var newItem = FactorioXmlHelper.ReadXml(reader);

            // check if this item is in the unknown list
            var item = unknownItems.Where(x => x.Name == newItem.Name).FirstOrDefault();
            if (item != null)
            {
                // exists in the unknown list
                // copy properties to existing 
                item.CraftingOutput = newItem.CraftingOutput;
                item.CraftingTime = newItem.CraftingTime;
                item.Productivity = newItem.Productivity;
                // remove item von unknown list
                unknownItems.Remove(item);
                // override the newItem instance with the existing unknown instance
                // all items which reference the unknown item are automaticaly updated with the new information
                newItem = item;
            }

            knownItems.Add(newItem);
            currentItem = newItem;
        }


        /// <summary>
        /// read a craft and add it to the current item 
        /// </summary>
        /// <param name="reader">xml reader with the current craft</param>
        /// <param name="currentItem">item where the craft gets added</param>
        /// <param name="knownItems">all known items</param>
        /// <param name="unknownItems">all unknown items</param>
        private void readCraftingElement(XmlReader reader, FactorioItem currentItem, List<FactorioItem> knownItems, List<FactorioItem> unknownItems)
        {
            if (currentItem != null)
            {
                // get the values
                var id = FactorioXmlHelper.ReadAttribute<int>(reader, FactorioXmlHelper.XmlCraftingAttributeId);
                var amount = FactorioXmlHelper.ReadAttribute<int>(reader, FactorioXmlHelper.XmlCraftingAttributeQuantity);

                // check if this item already exists in the known list
                var item = knownItems.Where(x => x.Id == id).FirstOrDefault();

                if (item == null)
                {
                    // check if this item already exists in the unknown list
                    item = unknownItems.Where(x => x.Id == id).FirstOrDefault();

                    if (item == null)
                    {
                        // if not create a new one and add it to the unknown list
                        item = new FactorioItem(id);
                        unknownItems.Add(item);
                    }
                }
                currentItem.AddRecipeItem(item, amount);
            }
        }


        #endregion

    }
}
