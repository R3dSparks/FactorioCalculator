using Factorio.Entities.Helper;
using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;

namespace Factorio.Entities
{
    /// <summary>
    /// This object represents one item in factorio
    /// </summary>
    [ImplementPropertyChanged]
    public class FactorioItem : INotifyPropertyChanged
    {
        #region Private Variables

        private static int idCounter = 0;

        private ObservableDictionary<FactorioItem, int> m_recipe;

        #endregion



        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Properties

        public int Id { get; private set; }

        /// <summary>
        /// name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Profuctivity means items per second
        /// </summary>
        public double Productivity { get; set; }

        /// <summary>
        /// The amount which is crafted with one craft
        /// </summary>
        public int CraftingOutput { get; set; }

        /// <summary>
        /// How long it takes to craft it
        /// </summary>
        public double CraftingTime { get; set; }

        /// <summary>
        /// Which <see cref="FactorioItem"/> are needed to craft this item
        /// </summary>
        public ObservableDictionary<FactorioItem, int> Recipe
        {
            get
            {
                if (m_recipe == null)
                    m_recipe = new ObservableDictionary<FactorioItem, int>();

                return m_recipe;
            }
        }

        /// <summary>
        /// Where this item is crafted
        /// </summary>
        public Crafting DefaultCraftingStation { get; set; }

        /// <summary>
        /// Path to the item picture
        /// </summary>
        public string PicturePath { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FactorioItem()
        {
            Id = idCounter++;
        }

        /// <summary>
        /// Initialize Factorio item with id
        /// </summary>
        /// <param name="id"></param>
        public FactorioItem(int id)
        {
            Id = id;

            if (Id >= idCounter)
                idCounter = Id + 1;
        }

        /// <summary>
        /// Create a <see cref="FactorioItem"/> with a name, craft amount and a crafting time
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="time"></param>
        public FactorioItem(string name, int output, double time, Crafting crafting = Crafting.AssemblingMachine, string path = null)
        {
            Id = idCounter++;
            Name = name;
            CraftingOutput = output;
            CraftingTime = time;
            Productivity = output / time;
            DefaultCraftingStation = crafting;
            PicturePath = path;
        }

        public FactorioItem(int id, string name, int output, double time, Crafting crafting = Crafting.AssemblingMachine, string path = null)
        {
            Id = id;
            Name = name;
            CraftingOutput = output;
            CraftingTime = time;
            Productivity = output / time;
            DefaultCraftingStation = crafting;
            PicturePath = path;
        }


        #endregion
        
        #region Public methods


        /// <summary>
        /// add a recipe item which is needed to craft this item
        /// </summary>
        /// <param name="item">the referenc to the item</param>
        /// <param name="quantity">the amount which is needed for the craft</param>
        public void AddRecipeItem(FactorioItem item, int quantity)
        {
            if (!Recipe.ContainsKey(item))
            {
                Recipe.Add(item, quantity);
            }
        }

        /// <summary>
        /// Remove an item from the recipe
        /// </summary>
        /// <param name="item"></param>
        public void RemoveRecipeItem(FactorioItem item)
        {
            Recipe.Remove(item);
        }

        /// <summary>
        /// Get copy of this item
        /// </summary>
        /// <returns></returns>
        public FactorioItem GetCopy()
        {
            FactorioItem copyItem = new FactorioItem(this.Id)
            {
                Name = this.Name,
                CraftingOutput = this.CraftingOutput,
                CraftingTime = this.CraftingTime,
                Productivity = this.Productivity,
                DefaultCraftingStation = this.DefaultCraftingStation,
                PicturePath = this.PicturePath
            };

            foreach (var recipeItem in this.Recipe)
            {
                copyItem.AddRecipeItem(recipeItem.Key, recipeItem.Value);
            }

            return copyItem;
        }

        #endregion 

    }
}
