using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (reader.Name == "Item" && reader.NodeType != XmlNodeType.EndElement)
                {
                    string temp = reader.GetAttribute("name");
                    items.Add(new Item(                       
                        reader.GetAttribute("name"), 
                        Convert.ToDouble(reader.GetAttribute("productivity"))
                        )
                    );
                }
            }

            reader.Close();

            reader = XmlReader.Create(path);

            Item currentItem = null;

            //Add crafts to items in the list
            while(reader.Read())
            {
                if(reader.Name == "Item" && !reader.IsEmptyElement)
                {
                    currentItem = items.Find(x => x.Name == reader.GetAttribute("name"));
                }
                else if(reader.Name == "Crafting")
                {
                    currentItem.AddRecipeItem(
                        items.Find(x => x.Name == reader.GetAttribute("item")),
                        Convert.ToInt32(reader.GetAttribute("quantity"))
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

            writer.WriteStartElement("Items");

            foreach(var item in items)
            {
                writer.WriteStartElement("Item");

                item.WriteXml(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();

        }

        public static void AddItem(Item item, string path)
        {
            List<Item> items = ReadItems(path);
            items.Add(item);
            SaveItems(items, path);
        }

        private static void createXmlFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(path, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement("Items");
            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Close();
        }
    }
}
