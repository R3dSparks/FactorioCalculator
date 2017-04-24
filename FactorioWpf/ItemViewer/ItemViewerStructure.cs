using Factorio;
using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf
{
    public static class ItemViewerStructure
    {
        /// <summary>
        /// Factorio logic to get item access
        /// </summary>
        private static IFactorioLogic factorioLogic = new FactorioLogic(@"..\..\..\Factorio.DAL\Files\ItemList.xml");

        /// <summary>
        /// Get all FactorioItems
        /// </summary>
        /// <returns></returns>
        public static List<FactorioItem> GetItems()
        {
            return factorioLogic.Items;
        }
    }
}
