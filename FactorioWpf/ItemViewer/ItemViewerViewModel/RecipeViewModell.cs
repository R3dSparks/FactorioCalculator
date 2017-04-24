using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioWpf
{
    public class RecipeViewModell : BaseViewModel
    {
        public Dictionary<FactorioItem, int> Recipe { get; set; }

        public RecipeViewModell(Dictionary<FactorioItem, int> recipe)
        {
            this.Recipe = recipe;
        }
    }
}
