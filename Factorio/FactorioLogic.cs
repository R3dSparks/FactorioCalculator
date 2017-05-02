using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Factorio.Entities;
using Factorio.DAL;
using System.Collections.ObjectModel;

namespace Factorio
{
    public class FactorioLogic : IFactorioLogic
    {
        private ObservableCollection<FactorioItem> m_items;
        private IFactorioXmlDal m_xmlDal;

        #region Properties

        /// <summary>
        /// Path to ItemList xml
        /// </summary>
        public string ItemListXmlPath { get; private set; }


        /// <summary>
        /// Contains all items of this application
        /// </summary>
        public ObservableCollection<FactorioItem> Items
        {
            get
            {
                if (m_items == null)
                    LoadItems();
                return m_items;
            }
            private set
            {
                m_items = value;
            }
        }

        

        /// <summary>
        /// public accessor for the xml dal
        /// </summary>
        public IFactorioXmlDal XmlDal
        {
            get
            {
                if (m_xmlDal == null)
                    m_xmlDal = new FactorioXmlDal();
                return m_xmlDal;
            }
            set
            {
                m_xmlDal = value;
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FactorioLogic()
        {

        }

        /// <summary>
        /// Create FactorioLogic with path to items file
        /// </summary>
        /// <param name="path">Path to items file</param>
        public FactorioLogic(string path)
        {
            ItemListXmlPath = path;
        }


        #endregion

        #region IO
        

        /// <summary>
        /// Read a file into the business layer
        /// </summary>
        /// <param name="path"></param>
        public void LoadItems()
        {
            this.Items = this.XmlDal.ReadItems(ItemListXmlPath);
        }


        /// <summary>
        /// Write a file from the business layer
        /// </summary>
        /// <param name="path"></param>
        public void SaveItems()
        {
            this.XmlDal.SaveItems(this.Items, ItemListXmlPath);
        }


        #endregion

        #region Public Methods

        public void AddRecipe(FactorioItem item, int quantity, FactorioItem recipeItem)
        {
            item.AddRecipeItem(recipeItem, quantity);
        }

        /// <summary>
        /// Remove an item and save Items list to xml file
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(FactorioItem item)
        {
            this.Items.Remove(item);
            this.SaveItems();
        }

        public int GetItemId(string name)
        {
            return this.Items.First(item => item.Name == name).Id;
        }

        /// <summary>
        /// Edit an existing item and save Items list to xml file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="time"></param>
        /// <param name="crafting"></param>
        /// <param name="path"></param>
        public void EditItem(FactorioItem item, string name, int output, double time, Crafting crafting, string path)
        {
            int index = Items.IndexOf(item);
            Dictionary<FactorioItem, int> recipe = item.Recipe;

            Items[index] = new FactorioItem(item.Id, name, output, time, crafting, path);
            Items[index].Recipe = recipe;


            this.SaveItems();
        }

        /// <summary>
        /// Add a new item to the Items list and save Items list to xml file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="time"></param>
        /// <param name="crafting"></param>
        /// <exception cref="FactorioException"></exception>
        public void AddItem(string name, int output, double time, Crafting crafting, string path)
        {
            // If item already exists throw exception
            if (this.Items.Any(i => i.Name == name))
            {
                throw new FactorioException(DiagnosticEvents.ItemAlreadyExists, "Item already exists!");
            }

            // Add item and save
            this.Items.Add(new FactorioItem(name, output, time, crafting, path));
            this.SaveItems();
        }

        #endregion
    }
}
