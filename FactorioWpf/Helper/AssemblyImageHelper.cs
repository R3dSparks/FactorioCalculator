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
        public FactorioAssembly Assembly { get; set; }

        /// <summary>
        /// Image width
        /// </summary>
        public static int Width { get; } = 30;

        /// <summary>
        /// Image height
        /// </summary>
        public static int Height { get; } = 30;

        /// <summary>
        /// Leftward offset from the image
        /// </summary>
        public static int TopOffset { get; } = 20;

        /// <summary>
        /// Upward offset from the image
        /// </summary>
        public static int LeftOffset { get; } = 20;

        public int Left { get; }

        public int Top { get; }

        public string ImagePath { get => Assembly.AssemblyItem.PicturePath; }

        public AssemblyImageHelper(int left, int top, FactorioAssembly assembly)
        {
            Left = left;
            Top = top;
            Assembly = assembly;
        }

    }
}
