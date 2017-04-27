using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Factorio.Entities;
using Factorio.DAL;

namespace Factorio
{
    public class FactorioLogic : IFactorioLogic
    {
        private List<FactorioItem> m_items;
        private IFactorioXmlDal m_xmlDal;
        private string ItemListXmlPath;

        #region Properties

        /// <summary>
        /// Contains all items of this application
        /// </summary>
        public List<FactorioItem> Items
        {
            get
            {
                if (m_items == null)
                    ReadFile();
                return m_items;
            }
            private set
            {
                m_items = value;
            }
        }

        public void AddItem(string argName, string argOutput, string argTime, object argCrafting)
        {
            string name;
            int output;
            double time;
            Crafting crafting;

            name = argName;
            output = Convert.ToInt32(argOutput);
            time = Convert.ToDouble(argTime);
            crafting = (Crafting)argCrafting;

            if (this.Items.Any(i => i.Name == name))
            {
                throw new FactorioException(DiagnosticEvents.ItemAlreadyExists, "Item already exists!");
            }

            // Add item and save
            this.Items.Add(new FactorioItem(name, output, time, crafting));
            this.WriteFile();
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
        /// default constructor
        /// </summary>
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
        public void ReadFile()
        {
            this.Items = this.XmlDal.ReadItems(ItemListXmlPath);
        }


        /// <summary>
        /// Write a file from the business layer
        /// </summary>
        /// <param name="path"></param>
        public void WriteFile()
        {
            this.XmlDal.SaveItems(this.Items, ItemListXmlPath);
        }
        

        #endregion

    }
}
