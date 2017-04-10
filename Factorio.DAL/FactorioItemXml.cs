using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using Factorio.Entities;
using System;

namespace Factorio.DAL
{
    /// <summary>
    /// This class extends the <see cref="FactorioItem"/> class with the needed functionality to save it.
    /// </summary>
    static class FactorioItemXmlExtenstion
    {

        #region Xml markers
        public static readonly string XmlMainElement = "Items";

        public static readonly string XmlItemElement = "Item";
        public static readonly string XmlItemAttributeName = "name";
        public static readonly string XmlItemAttributeOutput = "output";
        public static readonly string XmlItemAttributeTime = "time";

        public static readonly string XmlCraftingElement = "Crafting";
        public static readonly string XmlCraftingAttributeItem = "item";
        public static readonly string XmlCraftingAttributeQuantity = "quantity";
        #endregion

        #region XML IO Methods

        /// <summary>
        /// Fill this object with the information from the <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">contains the information for this object</param>
        public static void ReadXml(this FactorioItem item, XmlReader reader)
        {
            item.Name = reader.GetAttribute(FactorioItemXmlExtenstion.XmlItemAttributeName);
            item.CraftingOutput = Convert.ToInt32(reader.GetAttribute(FactorioItemXmlExtenstion.XmlItemAttributeOutput));
            item.CraftingTime = Convert.ToDouble(reader.GetAttribute(FactorioItemXmlExtenstion.XmlItemAttributeTime));
            item.Productivity = item.CraftingOutput / item.CraftingTime;
        }


        /// <summary>
        /// Add this object to the <see cref="writer"/>.
        /// </summary>
        /// <param name="writer"></param>
        public static void WriteXml(this FactorioItem item, XmlWriter writer)
        {
            writer.WriteAttributeString(FactorioItemXmlExtenstion.XmlItemAttributeName, item.Name);
            writer.WriteAttributeString(FactorioItemXmlExtenstion.XmlItemAttributeOutput, item.CraftingOutput.ToString());
            writer.WriteAttributeString(FactorioItemXmlExtenstion.XmlItemAttributeTime, item.CraftingTime.ToString());

            if (item.Recipe != null)
            {
                foreach (var craft in item.Recipe)
                {
                    writer.WriteStartElement(FactorioItemXmlExtenstion.XmlCraftingElement);
                    writer.WriteAttributeString(FactorioItemXmlExtenstion.XmlCraftingAttributeItem, craft.Key.Name);
                    writer.WriteAttributeString(FactorioItemXmlExtenstion.XmlCraftingAttributeQuantity, craft.Value.ToString());
                    writer.WriteEndElement();
                }
            }

        }


        #endregion
    }
}
