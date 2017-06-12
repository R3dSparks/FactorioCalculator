using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class Layer
    {

        #region Private Variables



        private List<AssemblyImageHelper> m_assemblyImages;

        private int m_width = 0;

        

        #endregion

        #region Properties



        /// <summary>
        /// Images which are located in this layer
        /// </summary>
        public List<AssemblyImageHelper> AssemblyImages
        {
            get
            {
                if (m_assemblyImages == null)
                    m_assemblyImages = new List<AssemblyImageHelper>();

                return m_assemblyImages;
            }
        }

        /// <summary>
        /// Depth of this layer in the tree structure
        /// </summary>
        public int Depth { get; set; }
        
        /// <summary>
        /// Width of this layer
        /// </summary>
        public int Width {
            get { return m_width; }
            set
            {
                m_width = value;
                if (m_width > MaxWidth)
                    MaxWidth = m_width;
            }
        }

        /// <summary>
        /// max with for what ever -> Sebastian was macht das hier??
        /// </summary>
        public int MaxWidth { get; set; } = 0;



        #endregion

        #region Constructors



        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="depth"></param>
        public Layer(int depth)
        {
            Depth = depth;
        }



        #endregion


        
        public AssemblyImageHelper AddAssembly(FactorioAssembly assembly, int position)
        {
            AssemblyImageHelper image = new AssemblyImageHelper(assembly);
            image.Top = this.Depth * image.Height;


            if (position >= this.Width)
            {
                image.Left = position;
                this.Width += (position - this.Width) + image.Width;
            }
            else
            {
                image.Left = this.Width;
                this.Width += image.Width;
            }

            this.AssemblyImages.Add(image);

            return image;

        }
    }
}
