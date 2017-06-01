using Factorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.Helper
{
    public class AssemblyImageHelper
    {
        /// <summary>
        /// Assembly that is displayed
        /// </summary>
        public FactorioAssembly Assembly { get; private set; }

        /// <summary>
        /// Image width
        /// </summary>
        public static int Width { get; private set; } = 30;

        /// <summary>
        /// Image height
        /// </summary>
        public static int Height { get; private set; } = 30;

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
        public string ImagePath { get => Assembly.AssemblyItem.ImagePath; }

        public AssemblyImageHelper(int left, int top, FactorioAssembly assembly)
        {
            Left = left;
            Top = top;
            Assembly = assembly;
        }

    }
}
