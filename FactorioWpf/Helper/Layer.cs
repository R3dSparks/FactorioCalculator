using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class Layer
    {
        private List<AssemblyImageHelper> m_images;

        public int Top { get; set; }



        public List<AssemblyImageHelper> Images
        {
            get
            {
                if (m_images == null)
                    m_images = new List<AssemblyImageHelper>();

                return m_images;
            }
        }

        public Layer(int top)
        {
            Top = top;
        }


    }
}
