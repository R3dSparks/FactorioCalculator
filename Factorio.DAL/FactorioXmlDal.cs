using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Factorio.Entities;
using System.Linq;

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


        /// <summary>
        /// Read all <see cref="FactorioItem"/> from a xml file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        public List<FactorioItem> ReadItems(string path)
        {
            if (!File.Exists(path))
                createXmlFile(path);

            var reader = XmlReader.Create(path);
            

            FactorioItem currentItem = null;
            // contains items where all properties are known
            var knownItems = new List<FactorioItem>();
            // contains items where only the name is known
            var unknownItems = new List<FactorioItem>();
            
            // read all lines
            while (reader.Read())
            {
                if (reader.Name == FactorioItemXmlExtenstion.XmlItemElement && reader.NodeType != XmlNodeType.EndElement)
                {
                    readItemElement(reader, out currentItem, knownItems, unknownItems);
                }
                else if (reader.Name == FactorioItemXmlExtenstion.XmlCraftingElement)
                {
                    readCraftingElement(reader, currentItem, knownItems, unknownItems);
                }
            }

            reader.Close();
            return knownItems;
        }


        /// <summary>
        /// Save a list of <see cref="FactorioItem"/> in a file. Existing files will be overriden
        /// </summary>
        /// <param name="items">save these <see cref="FactorioItem"/>s</param>
        /// <param name="path">save the file here</param>
        public void SaveItems(List<FactorioItem> items, string path)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(FactorioItemXmlExtenstion.XmlMainElement);

            foreach (var item in items)
            {
                writer.WriteStartElement(FactorioItemXmlExtenstion.XmlItemElement);

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

            writer.WriteStartElement(FactorioItemXmlExtenstion.XmlMainElement);
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
            var newItem = new FactorioItem().ReadXml(reader);

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
                string name = reader.GetAttribute(FactorioItemXmlExtenstion.XmlCraftingAttributeItem);
                int amount = Convert.ToInt32(reader.GetAttribute(FactorioItemXmlExtenstion.XmlCraftingAttributeQuantity));

                // check if this item already exists in the known list
                var item = knownItems.Where(x => x.Name == name).FirstOrDefault();
                if (item == null)
                {
                    // check if this item already exists in the unknown list
                    item = unknownItems.Where(x => x.Name == name).FirstOrDefault();
                    if (item == null)
                    {
                        // if not create a new one and add it to the unknown list
                        item = new FactorioItem();
                        item.Name = name;
                        unknownItems.Add(item);
                    }
                }
                currentItem.AddRecipeItem(item, amount);
            }
        }


        #endregion

    }
}
