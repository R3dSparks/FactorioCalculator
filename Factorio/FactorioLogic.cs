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
        private string ItemListXmlPath;

        #region Properties

        /// <summary>
        /// Contains all items of this application
        /// </summary>
        public ObservableCollection<FactorioItem> Items
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
            this.Items = new ObservableCollection<FactorioItem>(this.XmlDal.ReadItems(ItemListXmlPath));
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
