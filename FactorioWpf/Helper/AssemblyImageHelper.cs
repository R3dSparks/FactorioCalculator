using Factorio;
using Factorio.Entities.Interfaces.ProductionViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class AssemblyImageHelper : IPVImage
    {

        #region Private Variables



        private int m_widthOffset = 20;
        private int m_heightOffset = 40;



        #endregion

        #region Interface Properties



        /// <summary>
        /// Width of the image
        /// </summary>
        public int ImageWidth { get { return 30; } }

        /// <summary>
        /// Height of the image
        /// </summary>
        public int ImageHeight { get { return 30; } }


        /// <summary>
        /// Distance from left of canvas
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Distance from top of canvas
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath { get { return this.Assembly.AssemblyItem.ImagePath; } }



        #endregion

        #region Properties



        /// <summary>
        /// Assembly that is displayed
        /// </summary>
        public FactorioAssembly Assembly { get; private set; }

        /// <summary>
        /// Image width
        /// </summary>
        public int Width
        {
            get { return this.ImageWidth + m_widthOffset; }
        }

        /// <summary>
        /// Image height
        /// </summary>
        public int Height
        {
            get { return this.ImageHeight + m_heightOffset; }
        }



        #endregion

        #region Constructors



        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="assembly"></param>
        public AssemblyImageHelper(int left, int top, FactorioAssembly assembly)
        {
            this.Left = left;
            this.Top = top;
            this.Assembly = assembly;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyImageHelper(FactorioAssembly assembly)
        {
            this.Assembly = assembly;
        }



        #endregion


    }
}
