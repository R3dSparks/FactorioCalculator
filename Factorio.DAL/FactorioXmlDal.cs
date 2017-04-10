using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Factorio.Entities;

namespace Factorio.DAL
{

    public static class FactorioXmlDal
    {



        /// <summary>
        /// Read all <see cref="FactorioItem"/> from a xml file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        public static List<FactorioItem> ReadItems(string path)
        {
            if (!File.Exists(path))
                createXmlFile(path);

            var reader = XmlReader.Create(path);

            var items = new List<FactorioItem>();

            //Add all items to the list without crafts
            while(reader.Read())
            {
                if(reader.Name == FactorioItemXmlExtenstion.XmlItemElement && reader.NodeType != XmlNodeType.EndElement)
                {
                    var item = new FactorioItem();
                    item.ReadXml(reader);
                    items.Add(item);
                }               
            }

            reader.Close();

            reader = XmlReader.Create(path);

            FactorioItem currentItem = null;

            //Add crafts to items in the list
            while(reader.Read())
            {
                if(reader.Name == FactorioItemXmlExtenstion.XmlItemElement && !reader.IsEmptyElement)
                {
                    currentItem = items.Find(x => x.Name == reader.GetAttribute(FactorioItemXmlExtenstion.XmlItemAttributeName));
                }
                else if(reader.Name == FactorioItemXmlExtenstion.XmlCraftingElement)
                {
                    currentItem.AddRecipeItem(
                        items.Find(x => x.Name == reader.GetAttribute(FactorioItemXmlExtenstion.XmlCraftingAttributeItem)),
                        Convert.ToInt32(reader.GetAttribute(FactorioItemXmlExtenstion.XmlCraftingAttributeQuantity))
                    );
                }
                
            }

            reader.Close();

            return items;
        }


        /// <summary>
        /// Save a list of <see cref="FactorioItem"/> in a file. Existing files will be overriden
        /// </summary>
        /// <param name="items">save these <see cref="FactorioItem"/>s</param>
        /// <param name="path">save the file here</param>
        public static void SaveItems(List<FactorioItem> items, string path)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(FactorioItemXmlExtenstion.XmlMainElement);

            foreach(var item in items)
            {
                writer.WriteStartElement(FactorioItemXmlExtenstion.XmlItemElement);

                item.WriteXml(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();

        }


        /// <summary>
        /// create the file 
        /// </summary>
        /// <param name="path"></param>
        private static void createXmlFile(string path)
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
    }
}
