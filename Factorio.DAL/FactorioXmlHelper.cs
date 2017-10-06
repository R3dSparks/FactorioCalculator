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

        #region XML IO Methods

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

                int id = Convert.ToInt32(xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeId).Value);

                if (unknownItems.Exists(i => i.Id == id))
                {
                    item = unknownItems.Find(i => i.Id == id);

                    item.Name = xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeName).Value;
                    item.CraftingOutput = Convert.ToInt32(xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeOutput).Value);
                    item.CraftingTime = Convert.ToDouble(xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeTime).Value);
                    item.DefaultCraftingType = (CraftingType)Enum.Parse(typeof(CraftingType), xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeCraftingStation).Value);
                    item.ImagePath = xmlData.Attribute(FactorioXmlHelper.XmlItemAttributePicture).Value;

                    unknownItems.Remove(item);

                }
                else
                {
                    item = new FactorioItem(id)
                    {
                        Name = xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeName).Value,
                        CraftingOutput = Convert.ToInt32(xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeOutput).Value),
                        CraftingTime = Convert.ToDouble(xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeTime).Value),
                        DefaultCraftingType = (CraftingType)Enum.Parse(typeof(CraftingType), xmlData.Attribute(FactorioXmlHelper.XmlItemAttributeCraftingStation).Value),
                    };

                    if (xmlData.Attribute(FactorioXmlHelper.XmlItemAttributePicture) != null)
                        item.ImagePath = xmlData.Attribute(FactorioXmlHelper.XmlItemAttributePicture).Value;
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
        public static void TryAddRecipeData(this FactorioItem item, XElement xmlData, List<FactorioItem> knownItems, List<FactorioItem> unknownItems)
        {
            if(xmlData.Descendants(FactorioXmlHelper.XmlCraftingElement).FirstOrDefault() != null)
            {
                try
                {
                    // Get a list of all recipe parts of XElement
                    var recipes = xmlData.Descendants(FactorioXmlHelper.XmlCraftingElement);
                    
                    foreach (var recipe in recipes)
                    {
                        // Id of the recipe item
                        int id = Convert.ToInt32(recipe.Attribute(FactorioXmlHelper.XmlCraftingAttributeId).Value);

                        // Quantity of the recipe item
                        int quantity = Convert.ToInt32(recipe.Attribute(FactorioXmlHelper.XmlCraftingAttributeQuantity).Value);

                        // Look for a known item with id of the recipe item
                        FactorioItem recipeItem = knownItems.Find(i => i.Id == id);

                        if (recipeItem != null)
                        {
                            item.AddRecipeItem(recipeItem, quantity);
                        }
                        // If the recipe item is not known create a dummy item with its id and add it to unkown items
                        else if(unknownItems.Exists(i => i.Id == id) == false)
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
        public static void WriteXml(this FactorioItem item, XmlWriter writer)
        {
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeId, item.Id.ToString());
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeName, item.Name);
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeOutput, item.CraftingOutput.ToString());
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeTime, item.CraftingTime.ToString());
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeCraftingStation, item.DefaultCraftingType.ToString());

            if(item.ImagePath != null)
                writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributePicture, item.ImagePath);

            if (item.Recipe != null)
            {
                foreach (var craft in item.Recipe)
                {
                    writer.WriteStartElement(FactorioXmlHelper.XmlCraftingElement);
                    writer.WriteAttributeString(FactorioXmlHelper.XmlCraftingAttributeId, craft.Key.Id.ToString());
                    writer.WriteAttributeString(FactorioXmlHelper.XmlCraftingAttributeQuantity, craft.Value.ToString());
                    writer.WriteEndElement();
                }
            }

        }

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
        /// Create a standart xml file to save items
        /// </summary>
        /// <param name="path"></param>
        public static void CreateXml(string path)
        {
            XDocument doc = new XDocument();

            doc.Add(new XElement(XmlMainElement));

            doc.Save(path);
        }
    }
}
