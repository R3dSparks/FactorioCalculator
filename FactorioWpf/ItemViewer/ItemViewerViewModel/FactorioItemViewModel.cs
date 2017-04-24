using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FactorioWpf
{
    public class FactorioItemViewModel : BaseViewModel
    {
        private FactorioItem factorioItem { get; set; }

        public string Name { get { return factorioItem.Name; } }

        public List<string> FactorioItemProperties { get; set; }

        public ObservableCollection<RecipeViewModell>  FactorioItemRecipe { get; set; }

        public bool IsExpanded
        {
            get => FactorioItemProperties?.Count > 0;
            set
            {
                if (value == true)
                    this.Expand();
                else
                {
                    this.FactorioItemProperties = new List<string>();
                    this.FactorioItemRecipe = new ObservableCollection<RecipeViewModell>();
                    if (factorioItem.Recipe != null)
                        //Add dummy item
                        FactorioItemRecipe.Add(null);
                }
            }
        }


        public FactorioItemViewModel(FactorioItem item)
        {
            this.factorioItem = item;
        }

        public void Expand()
        {
            FactorioItemProperties.Add($"Name: {factorioItem.Name}");
            FactorioItemProperties.Add($"Output: {factorioItem.CraftingOutput}");

            FactorioItemRecipe.Add(new RecipeViewModell(factorioItem.Recipe));
        }
    }
}
