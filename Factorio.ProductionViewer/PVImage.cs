using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper.ProductionViewer
{
    /// <summary>
    /// This class represents one image in the prouction viewer
    /// </summary>
    public class PVImage : IPVImage
    {

        #region Private variables


        FactorioAssembly m_relatedAssembly;
        PVSettings m_settings;


        #endregion

        #region Interface Properties



        /// <summary>
        /// Width of the image
        /// </summary>
        public int ImageWidth
        {
            get { return this.ViewSettings.ImageWidth; }
        }

        /// <summary>
        /// Height of the image
        /// </summary>
        public int ImageHeight
        {
            get { return this.ViewSettings.ImageHeight; }
        }

        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        public int Left
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath
        {
            get { return this.PatrentAssembly.AssemblyItem.ImagePath; }
        }


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
        public PVSettings ViewSettings {
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
        public PVImage(FactorioAssembly assambly, PVSettings setting)
        {
            this.PatrentAssembly = assambly;
            this.ViewSettings = setting;
        }



        #endregion

        
    }
}
