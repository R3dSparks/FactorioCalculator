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
    static class FactorioXmlHelper
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
        public static FactorioItem ReadXml(this FactorioItem item, XmlReader reader)
        {
            item.Name = FactorioXmlHelper.ReadAttribute<string>(reader, FactorioXmlHelper.XmlItemAttributeName);
            item.CraftingOutput = FactorioXmlHelper.ReadAttribute<int>(reader, FactorioXmlHelper.XmlItemAttributeOutput);
            item.CraftingTime = FactorioXmlHelper.ReadAttribute<double>(reader, FactorioXmlHelper.XmlItemAttributeTime);
            item.Productivity = item.CraftingOutput / item.CraftingTime;
            return item;
        }


        /// <summary>
        /// read an attribute from a <see cref="XmlReader"/> and convert it into the type <paramref name="T"/>
        /// </summary>
        /// <typeparam name="T">convert the read item into this type</typeparam>
        /// <param name="reader">this reader contains the attribute</param>
        /// <param name="attributeName">read the attribute with this name</param>
        /// <returns>returns the read item or throws an exception if someting isn't right</returns>
        /// <exception cref="FactorioException"></exception>
        public static T ReadAttribute<T>(XmlReader reader, string attributeName)
        {
            if (reader == null)
                throw new FactorioException(DiagnosticEvents.DalXmlReadAttribute, "Cannot read the attribute value, because the reade is empty.");

            if (reader.HasAttributes == false)
                throw new FactorioException(DiagnosticEvents.DalXmlReadAttribute, "The reader does not have any attributes.");

            string val = reader.GetAttribute(attributeName);
            if (FactorioXmlHelper.CanChangeType(val, typeof(T)) == false)
                throw new FactorioException(DiagnosticEvents.DalXmlReadAttribute, String.Format("The read information from the XmlReader cannot be Converted to the type '{0}'. The value is '{1}'", typeof(T), val));
            
            return (T)Convert.ChangeType(val, typeof(T));
        }


        /// <summary>
        /// Check if the object can be converted to the <paramref name="conversionType"/>. Return true if it is possible, otherwise false
        /// </summary>
        /// <param name="value">check if this object can be converted into the given type</param>
        /// <param name="conversionType">check if the object can be converted to this type</param>
        /// <returns>true if possible, false if not</returns>
        public static bool CanChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
            {
                return false;
            }

            if (value == null)
            {
                return false;
            }

            IConvertible convertible = value as IConvertible;

            if (convertible == null)
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// Add this object to the <see cref="writer"/>.
        /// </summary>
        /// <param name="writer"></param>
        public static void WriteXml(this FactorioItem item, XmlWriter writer)
        {
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeName, item.Name);
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeOutput, item.CraftingOutput.ToString());
            writer.WriteAttributeString(FactorioXmlHelper.XmlItemAttributeTime, item.CraftingTime.ToString());

            if (item.Recipe != null)
            {
                foreach (var craft in item.Recipe)
                {
                    writer.WriteStartElement(FactorioXmlHelper.XmlCraftingElement);
                    writer.WriteAttributeString(FactorioXmlHelper.XmlCraftingAttributeItem, craft.Key.Name);
                    writer.WriteAttributeString(FactorioXmlHelper.XmlCraftingAttributeQuantity, craft.Value.ToString());
                    writer.WriteEndElement();
                }
            }

        }


        #endregion
    }
}
