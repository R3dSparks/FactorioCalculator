using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf.ViewModels
{
    public class AddItemViewModell : BaseViewModell
    {
        private IFactorioLogic logic;

        private string[] craftingItemCrafting;

        public string TxtItemName { get; set; }

        public string TxtItemOutput { get; set; }

        public string TxtItemTime { get; set; }

        public string[] CraftingItemCrafting
        {
            get
            {
                if (craftingItemCrafting == null)
                    craftingItemCrafting = Enum.GetNames(typeof(Crafting));

                return craftingItemCrafting;
            }
        }


        public AddItemViewModell()
        {
            //this.logic = logic;
        }
    }
}
