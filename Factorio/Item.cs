using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Factorio
{
    public class Item : IXmlSerializable
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

        #region Properties
        public string Name { get; private set; }

        public double Productivity { get; set; }

        public int Output { get; set; }

        public double Time { get; set; }

        public Dictionary<Item, int> Recipe { get; private set; }

        public Crafting MadeIn { get; set; }

        #endregion

        #region Constructors
        public Item()
        {

        }

        public Item(string name, int output, double time)
        {
            Name = name;
            Output = output;
            Time = time;
            Productivity = output / time;
        }
        #endregion

        #region Public methods
        public void AddRecipeItem(Item item, int quantity)
        {
            if (Recipe == null)
                Recipe = new Dictionary<Item, int>();

            if(!Recipe.ContainsKey(item))
            {
                Recipe.Add(item, quantity);
            }
                
        }
    #endregion

        #region IXmlSerializable
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            this.Name = reader.GetAttribute(XmlItemAttributeName);
            this.Output = Convert.ToInt32(reader.GetAttribute(XmlItemAttributeOutput));
            this.Time = Convert.ToDouble(reader.GetAttribute(XmlItemAttributeTime));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlItemAttributeName, this.Name);
            writer.WriteAttributeString(XmlItemAttributeOutput, this.Output.ToString());
            writer.WriteAttributeString(XmlItemAttributeTime, this.Time.ToString());
            
            if(this.Recipe != null)
            {
                foreach(var craft in this.Recipe)
                {
                    writer.WriteStartElement(XmlCraftingElement);
                    writer.WriteAttributeString(XmlCraftingAttributeItem, craft.Key.Name);
                    writer.WriteAttributeString(XmlCraftingAttributeQuantity, craft.Value.ToString());
                    writer.WriteEndElement();
                }
            }

        }
        #endregion
    }
}
