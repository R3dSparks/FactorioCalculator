using Factorio.Entities;
using Factorio.Entities.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FactorioWpf.ViewModels
{
    public class ItemEditorViewModell : BaseViewModell
    {
        #region Private Variables

        /// <summary>
        /// Dummy item for editing
        /// </summary>
        private FactorioItem m_itemDummy;

        /// <summary>
        /// Names of <see cref="CraftingType"/>
        /// </summary>
        private string[] m_comboBoxItemCrafting;

        /// <summary>
        /// Logic for interaction with business layer
        /// </summary>
        private IFactorioLogic m_fLogic;

        /// <summary>
        /// Window this view modell is displayed in
        /// </summary>
        private Window m_currentWindow;

        /// <summary>
        /// Item id if item is edited
        /// </summary>
        private FactorioItem m_item;

        #endregion

        #region Commands

        /// <summary>
        /// Command for ContextMenu Delete on recipe item
        /// </summary>
        private RelayCommand m_deleteRecipeItem_Click;

        /// <summary>
        /// Command for Ok button
        /// </summary>
        private RelayCommand m_addItemOk_Click;

        /// <summary>
        /// Command for Cancel button
        /// </summary>
        private RelayCommand m_addItemCancel_Click;

        /// <summary>
        /// Command for Add Picture button
        /// </summary>
        private RelayCommand m_addItemPicture_Click;

        /// <summary>
        /// Command for Recipe button
        /// </summary>
        private RelayCommand m_editItemRecipe_Click;

        #endregion

        #region Public Properties

        #region ICommands

        /// <summary>
        /// ICommand binded to the ContextMenu delete button on recipe
        /// </summary>
        public ICommand DeleteRecipeItem_Click
        {
            get
            {
                if (m_deleteRecipeItem_Click == null)
                    m_deleteRecipeItem_Click = new RelayCommand(DeleteRecipeItem);

                return m_deleteRecipeItem_Click;

            }
        }

        /// <summary>
        /// ICommand binded to the Recipe button
        /// </summary>
        public ICommand AddItemRecipe_Click
        {
            get
            {
                if (m_editItemRecipe_Click == null)
                    m_editItemRecipe_Click = new RelayCommand(AddRecipeItem);

                return m_editItemRecipe_Click;

            }
        }

        /// <summary>
        /// ICommand binded to the Add Picture button
        /// </summary>
        public ICommand AddItemPicture_Click
        {
            get
            {
                if (m_addItemPicture_Click == null)
                    m_addItemPicture_Click = new RelayCommand(AddPicture);

                return m_addItemPicture_Click;
            }
        }

        /// <summary>
        /// ICommand binded to the Ok button
        /// </summary>
        public ICommand AddItemOk_Click
        {
            get
            {
                if (m_addItemOk_Click == null)
                    m_addItemOk_Click = new RelayCommand(Ok_Click);

                return m_addItemOk_Click;
            }
        }

        /// <summary>
        /// ICommand binded to the Cancel button
        /// </summary>
        public ICommand AddItemCancel_Click
        {
            get
            {
                if (m_addItemCancel_Click == null)
                    m_addItemCancel_Click = new RelayCommand(Cancel_Click);

                return m_addItemCancel_Click;
            }
        }

        #endregion

        #region Visual Properties

        public string TxtRecipeQuantity { get; set; }

        /// <summary>
        /// Window title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Path to the item picture
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// TextBox for item name
        /// </summary>
        public string TxtItemName { get; set; }

        /// <summary>
        /// TextBox for item output
        /// </summary>
        public string TxtItemOutput { get; set; }

        /// <summary>
        /// TextBox for item time
        /// </summary>
        public string TxtItemTime { get; set; }

        /// <summary>
        /// ComboBox for selecting crafting station
        /// </summary>
        public string[] ComboBoxItemCrafting
        {
            get
            {
                if (m_comboBoxItemCrafting == null)
                    m_comboBoxItemCrafting = Enum.GetNames(typeof(CraftingType));

                return m_comboBoxItemCrafting;
            }
        }

        /// <summary>
        /// ComboBox for selecting a crafting item
        /// </summary>
        public List<FactorioItem> ComboBoxRecipeItems
        {
            get
            {
                // If no items are added yet, don't return list, because Xaml crashes
                if(m_fLogic.Items.Count > 0)
                    // Get all items without the item that is edited
                    return m_fLogic.Items.Where(x => x.Id != m_item.Id).ToList();

                return null;
            }
        }

        #endregion

        public FactorioItem ItemDummy
        {
            get
            {
                return m_itemDummy;
            }
        }

        /// <summary>
        /// Current item
        /// </summary>
        public FactorioItem Item
        {
            get
            {
                return m_item;
            }
        }

        /// <summary>
        /// Currently selected crafting station
        /// </summary>
        public string SelectedCrafting { get; set; } = CraftingType.AssemblingMachine.ToString();

        public KeyValuePair<FactorioItem, int> SelectedRecipeItem { get; set; }

        public FactorioItem SelectedComboBoxRecipeItem { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for item creation
        /// </summary>
        public ItemEditorViewModell(IFactorioLogic logic, Window window)
        {
            init(logic, window);

            m_item = new FactorioItem();
            m_itemDummy = new FactorioItem(-1);
        }

        /// <summary>
        /// Constructor for item editing
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="window"></param>
        /// <param name="item"></param>
        public ItemEditorViewModell(IFactorioLogic logic, Window window, FactorioItem item)
        {
            init(logic, window);

            this.Title = "Edit item";
            this.TxtItemName = item.Name;
            this.TxtItemOutput = item.CraftingOutput.ToString();
            this.TxtItemTime = item.CraftingTime.ToString();
            this.SelectedCrafting = item.DefaultCraftingType.ToString();
            this.PicturePath = item.PicturePath;

            this.m_item = item;
            this.m_itemDummy = item.GetCopy();           
        }
        
        /// <summary>
        /// Initializer for constructors
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="window"></param>
        private void init(IFactorioLogic logic, Window window)
        {
            this.m_fLogic = logic;
            this.m_currentWindow = window;

            // Select default recipe item
            if(m_fLogic.Items.Count > 0)
                SelectedComboBoxRecipeItem = m_fLogic.Items[0];
        }

        #endregion

        #region Command Methods

        private void DeleteRecipeItem()
        {
            m_itemDummy.RemoveRecipeItem(SelectedRecipeItem.Key);
        }

        private void AddRecipeItem()
        {
            int quantity = Convert.ToInt32(TxtRecipeQuantity);

            m_itemDummy.AddRecipeItem(SelectedComboBoxRecipeItem, quantity);
        }

        /// <summary>
        /// Add path to picture for the item
        /// </summary>
        private void AddPicture()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Image files (*.png) | *.png"
            };

            fileDialog.ShowDialog();

            if(string.IsNullOrEmpty(fileDialog.FileName) == false)
                PicturePath = fileDialog.FileName;
        }

        /// <summary>
        /// Close the window on cancel click
        /// </summary>
        private void Cancel_Click()
        {
            m_currentWindow?.Close();
        }

        /// <summary>
        /// Try creating a new item. Closing the window if successfull, else show error message.
        /// </summary>
        private void Ok_Click()
        {

            // Check if entries are existing
            if (
                TxtItemName == "" || string.IsNullOrWhiteSpace(TxtItemName) ||
                TxtItemOutput == null || TxtItemOutput == "" ||
                TxtItemTime == null || TxtItemTime == ""
              )
                throw new ArgumentNullException("Enter a value.");

            // Convert entries
            string name = TxtItemName;
            int output = Convert.ToInt32(TxtItemOutput);
            double time = Convert.ToDouble(TxtItemTime);
            CraftingType crafting = (CraftingType)Enum.Parse(typeof(CraftingType), SelectedCrafting);

            m_item.Name = name;
            m_item.CraftingOutput = output;
            m_item.CraftingTime = time;
            m_item.DefaultCraftingType = crafting;
            m_item.PicturePath = this.PicturePath;

            m_item.Recipe.Clear();

            // Copy recipe from the dummy item to the original
            foreach (var recipeItem in m_itemDummy.Recipe)
            {
                m_item.AddRecipeItem(recipeItem.Key, recipeItem.Value);
            }

            if(m_itemDummy.Id == -1)
                m_fLogic.Items.Add(m_item);

            m_fLogic.SaveItems();

            m_currentWindow?.Close();
        }

        #endregion
    }
}
