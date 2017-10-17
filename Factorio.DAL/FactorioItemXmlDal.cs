using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Factorio.DAL
{
    public class FactorioItemXmlDal : IFactorioXmlDal
    {

        #region Xml markers
        public static readonly string XmlMainElement = "Items";

        public static readonly string XmlItemElement = "Item";
        public static readonly string XmlItemAttributeId = "id";
        public static readonly string XmlItemAttributeName = "name";
        public static readonly string XmlItemAttributeOutput = "output";
        public static readonly string XmlItemAttributeTime = "time";
        public static readonly string XmlItemAttributeCraftingStation = "crafting";
        public static readonly string XmlItemAttributePicture = "picture";

        public static readonly string XmlCraftingElement = "Crafting";
        public static readonly string XmlCraftingAttributeId = "id";
        public static readonly string XmlCraftingAttributeQuantity = "quantity";
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
                    FactorioXmlHelper.CreateXml(path, XmlMainElement);
                }

                XDocument itemsFile = XDocument.Load(path);

                foreach (var xmlItemData in itemsFile.Descendants(XmlItemElement))
                {
                    FactorioItem newItem = GetFactorioItemFromXmlData(xmlItemData, knownItems, unknownItems);

                    knownItems.Add(newItem);

                    TryAddRecipeData(newItem, xmlItemData, knownItems, unknownItems);
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

            writer.WriteStartElement(XmlMainElement);

            foreach (var item in items)
            {
                writer.WriteStartElement(XmlItemElement);

                WriteItemXml(item, writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();

        }

        /// <summary>
        /// Create new <see cref="FactorioItem"/> from data in the XElement
        /// </summary>
        /// <param name="xmlData">Xml data for the item</param>
        /// <returns>New <see cref="FactorioItem"/></returns>
        public static FactorioItem GetFactorioItemFromXmlData(XElement xmlData, List<FactorioItem> knownItems, List<FactorioItem> unknownItems)
        {
            FactorioItem item;

            try
            {

                int id = Convert.ToInt32(xmlData.Attribute(XmlItemAttributeId).Value);

                if (unknownItems.Exists(i => i.Id == id))
                {
                    item = unknownItems.Find(i => i.Id == id);

                    item.Name = xmlData.Attribute(XmlItemAttributeName).Value;
                    item.CraftingOutput = Convert.ToInt32(xmlData.Attribute(XmlItemAttributeOutput).Value);
                    item.CraftingTime = Convert.ToDouble(xmlData.Attribute(XmlItemAttributeTime).Value);
                    item.DefaultCraftingType = (CraftingType)Enum.Parse(typeof(CraftingType), xmlData.Attribute(XmlItemAttributeCraftingStation).Value);
                    item.ImagePath = xmlData.Attribute(XmlItemAttributePicture).Value;

                    unknownItems.Remove(item);

                }
                else
                {
                    item = new FactorioItem(id)
                    {
                        Name = xmlData.Attribute(XmlItemAttributeName).Value,
                        CraftingOutput = Convert.ToInt32(xmlData.Attribute(XmlItemAttributeOutput).Value),
                        CraftingTime = Convert.ToDouble(xmlData.Attribute(XmlItemAttributeTime).Value),
                        DefaultCraftingType = (CraftingType)Enum.Parse(typeof(CraftingType), xmlData.Attribute(XmlItemAttributeCraftingStation).Value),
                    };

                    if (xmlData.Attribute(XmlItemAttributePicture) != null)
                        item.ImagePath = xmlData.Attribute(XmlItemAttributePicture).Value;
                }

                if (FactorioXmlHelper.IsImagePathValid(item.ImagePath) == false)
                    item.ImagePath = String.Empty;

            }
            catch (Exception)
            {
                throw;
            }

            return item;
        }

        /// <summary>
        /// Try to add a recipe to the this <see cref="FactorioItem"/>
        /// </summary>
        /// <param name="item">Item to add reipe for</param>
        /// <param name="xmlData">Xml data for this item</param>
        /// <param name="knownItems">List of all known items</param>
        /// <param name="unknownItems">List of all unkown items</param>
        public static void TryAddRecipeData(FactorioItem item, XElement xmlData, List<FactorioItem> knownItems, List<FactorioItem> unknownItems)
        {
            if (xmlData.Descendants(XmlCraftingElement).FirstOrDefault() != null)
            {
                try
                {
                    // Get a list of all recipe parts of XElement
                    var recipes = xmlData.Descendants(XmlCraftingElement);

                    foreach (var recipe in recipes)
                    {
                        // Id of the recipe item
                        int id = Convert.ToInt32(recipe.Attribute(XmlCraftingAttributeId).Value);

                        // Quantity of the recipe item
                        int quantity = Convert.ToInt32(recipe.Attribute(XmlCraftingAttributeQuantity).Value);

                        // Look for a known item with id of the recipe item
                        FactorioItem recipeItem = knownItems.Find(i => i.Id == id);

                        if (recipeItem != null)
                        {
                            item.AddRecipeItem(recipeItem, quantity);
                        }
                        // If the recipe item is not known create a dummy item with its id and add it to unkown items
                        else if (unknownItems.Exists(i => i.Id == id) == false)
                        {
                            recipeItem = new FactorioItem(id);

                            unknownItems.Add(recipeItem);

                            item.AddRecipeItem(recipeItem, quantity);
                        }
                    }

                }
                catch (FormatException)
                {
                    throw;
                }

            }
        }

        /// <summary>
        /// Add this object to the <see cref="writer"/>.
        /// </summary>
        /// <param name="writer"></param>
        public static void WriteItemXml(FactorioItem item, XmlWriter writer)
        {
            writer.WriteAttributeString(XmlItemAttributeId, item.Id.ToString());
            writer.WriteAttributeString(XmlItemAttributeName, item.Name);
            writer.WriteAttributeString(XmlItemAttributeOutput, item.CraftingOutput.ToString());
            writer.WriteAttributeString(XmlItemAttributeTime, item.CraftingTime.ToString());
            writer.WriteAttributeString(XmlItemAttributeCraftingStation, item.DefaultCraftingType.ToString());

            if (item.ImagePath != null)
                writer.WriteAttributeString(XmlItemAttributePicture, item.ImagePath);

            if (item.Recipe != null)
            {
                foreach (var craft in item.Recipe)
                {
                    writer.WriteStartElement(XmlCraftingElement);
                    writer.WriteAttributeString(XmlCraftingAttributeId, craft.Key.Id.ToString());
                    writer.WriteAttributeString(XmlCraftingAttributeQuantity, craft.Value.ToString());
                    writer.WriteEndElement();
                }
            }

        }

        #endregion
    }
}
