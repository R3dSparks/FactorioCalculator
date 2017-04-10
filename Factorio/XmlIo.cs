using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Factorio
{
    public static class XmlIO
    {    

        public static List<Item> ReadItems(string path)
        {
            if (!File.Exists(path))
                createXmlFile(path);

            XmlReader reader = XmlReader.Create(path);

            List<Item> items = new List<Item>();

            //Add all items to the list without crafts
            while(reader.Read())
            {
                if(reader.Name == Item.XmlItemElement && reader.NodeType != XmlNodeType.EndElement)
                {
                    Item item = new Item();
                    item.ReadXml(reader);
                    items.Add(item);
                }               
            }

            reader.Close();

            reader = XmlReader.Create(path);

            Item currentItem = null;

            //Add crafts to items in the list
            while(reader.Read())
            {
                if(reader.Name == Item.XmlItemElement && !reader.IsEmptyElement)
                {
                    currentItem = items.Find(x => x.Name == reader.GetAttribute(Item.XmlItemAttributeName));
                }
                else if(reader.Name == Item.XmlCraftingElement)
                {
                    currentItem.AddRecipeItem(
                        items.Find(x => x.Name == reader.GetAttribute(Item.XmlCraftingAttributeItem)),
                        Convert.ToInt32(reader.GetAttribute(Item.XmlCraftingAttributeQuantity))
                    );
                }
                
            }

            reader.Close();

            return items;
        }

        public static void SaveItems(List<Item> items, string path)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(Item.XmlMainElement);

            foreach(var item in items)
            {
                writer.WriteStartElement(Item.XmlItemElement);

                item.WriteXml(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();

        }

        private static void createXmlFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement(Item.XmlMainElement);
            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Close();
        }
    }
}
