using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class ProductionViewImage
    {

        #region Private variables


        FactorioAssembly m_relatedAssembly;
        ProductionViewSettings m_settings;


        #endregion

        #region Properties


        /// <summary>
        /// Reference to the Assembly which is represented by this class
        /// </summary>
        public FactorioAssembly PatrentAssembly
        {
            get { return m_relatedAssembly; }
            private set { m_relatedAssembly = value; }
        }

        /// <summary>
        /// Reference to the settings class
        /// </summary>
        public ProductionViewSettings ViewSettings {
            get { return m_settings; }
            private set { m_settings = value; }
        }

        /// <summary>
        /// The amount of images which are located left to the current image to calculate the spaceing. 
        /// It does not represent the total value of images located left.
        /// </summary>
        public int ImageLeftCount { get; set; }

        /// <summary>
        /// The amount of sub images below the current image.
        /// </summary>
        public int SubImageWith { get; set; }


        #endregion

        #region Constructions


        /// <summary>
        /// default consturctior
        /// </summary>
        /// <param name="assambly">this is the parrent assembly</param>
        /// <param name="setting">view settings class reference</param>
        public ProductionViewImage(FactorioAssembly assambly, ProductionViewSettings setting)
        {
            this.PatrentAssembly = assambly;
            this.ViewSettings = setting;
        }



        #endregion




    }
}
