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

        #region Properties


        /// <summary>
        /// Contains all items of this application
        /// </summary>
        public List<FactorioItem> Items
        {
            get
            {
                if (m_items == null)
                    m_items = new List<FactorioItem>();
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
        public FactorioLogic()
        {

        }


        #endregion

        #region IO
        

        /// <summary>
        /// Read a file into the business layer
        /// </summary>
        /// <param name="path"></param>
        public void ReadFile(string path)
        {
            this.Items = this.XmlDal.ReadItems(path);
        }


        /// <summary>
        /// Write a file from the business layer
        /// </summary>
        /// <param name="path"></param>
        public void WriteFile(string path)
        {
            this.XmlDal.SaveItems(this.Items, path);
        }
        

        #endregion

    }
}
