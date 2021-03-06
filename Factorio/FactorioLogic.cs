﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Factorio.Entities;
using Factorio.DAL;
using System.Collections.ObjectModel;
using Factorio.Entities.Interfaces;

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

        public IFactorioSettings Settings { get; set; }

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
                    m_xmlDal = new FactorioItemXmlDal();
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
        /// <param name="path">Basepath to FactorioCalculator folder</param>
        public FactorioLogic(string path)
        {
            ItemListXmlPath = path + @"\Data\ItemList.xml";
            Settings = new FactorioSettings(path + @"\Settings.xml");
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


        public void RemoveRecipe(FactorioItem item, FactorioItem recipeItem)
        {
            item.RemoveRecipeItem(recipeItem);
        }


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

            foreach (var i in this.Items)
            {
                i.RemoveRecipeItem(item);
            }

            this.SaveItems();
        }

        public int GetItemId(string name)
        {
            return this.Items.First(item => item.Name == name).Id;
        }

        /// <summary>
        /// Add a new item to the Items list and save Items list to xml file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="time"></param>
        /// <param name="crafting"></param>
        /// <exception cref="FactorioException"></exception>
        public void AddItem(string name, int output, double time, CraftingType crafting, string path)
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
