using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.ViewModels
{
    public class ItemDetailViewModell : BaseViewModell
    {

        #region Public Properties

        public FactorioItem Item { get; set; }

        #endregion

        public ItemDetailViewModell(FactorioItem item)
        {
            Item = item;
        }

    }
}
