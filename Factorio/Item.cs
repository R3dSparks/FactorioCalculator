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
        public static readonly string XmlItemAttributeProductivity = "productivity";

        public static readonly string XmlCraftingElement = "Crafting";
        public static readonly string XmlCraftingAttributeItem = "item";
        public static readonly string XmlCraftingAttributeQuantity = "quantity";
        #endregion

        #region Properties
        public string Name { get; private set; }

        public double Productivity { get; set; }

        public Dictionary<Item, int> Recipe { get; private set; }

        public Crafting MadeIn { get; set; }

        #endregion

        #region Constructors
        public Item()
        {

        }

        public Item(string name, double productivity)
        {
            Name = name;
            Productivity = productivity;
        }

        public Item(string name, int quantity, double crafttime)
        {
            Name = name;
            Productivity = quantity / crafttime;
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
            reader.MoveToContent();
            this.Name = reader.GetAttribute("name");

            bool isEmptyElement = reader.IsEmptyElement;
            reader.ReadStartElement();

            if(!isEmptyElement)
            {

            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(XmlItemAttributeName, this.Name);
            writer.WriteAttributeString(XmlItemAttributeProductivity, this.Productivity.ToString());
            
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
