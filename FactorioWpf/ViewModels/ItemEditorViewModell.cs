﻿using Factorio.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FactorioWpf.ViewModels
{
    public class ItemEditorViewModell : BaseViewModell
    {
        #region Private Variables

        /// <summary>
        /// Names of <see cref="Crafting"/>
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

        /// <summary>
        /// Wether editor is in editing or creating mode
        /// </summary>
        private bool m_editing = false;

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
                    m_addItemOk_Click = new RelayCommand(CreateItem);

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
                    m_addItemCancel_Click = new RelayCommand(Cancel);

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
                    m_comboBoxItemCrafting = Enum.GetNames(typeof(Crafting));

                return m_comboBoxItemCrafting;
            }
        }

        /// <summary>
        /// ComboBox for selecting a crafting item
        /// </summary>
        public ObservableCollection<FactorioItem> ComboBoxRecipeItems
        {
            get
            {
                // If no items are added yet, don't return list, because Xaml crashes
                if(m_fLogic.Items.Count > 0)
                    return m_fLogic.Items;

                return null;
            }
        }

        #endregion

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
        public string SelectedCrafting { get; set; } = Crafting.AssemblingMachine1.ToString();

        public KeyValuePair<FactorioItem, int> SelectedRecipe { get; set; }

        public FactorioItem SelectedComboBoxRecipeItem { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ItemEditorViewModell(IFactorioLogic logic, Window window)
        {
            init(logic, window);
        }

        public ItemEditorViewModell(IFactorioLogic logic, Window window, FactorioItem item)
        {
            init(logic, window);

            this.m_editing = true;
            this.m_item = item;           
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
            m_fLogic.RemoveRecipe(Item, SelectedRecipe.Key);
        }

        private void AddRecipeItem()
        {
            int quantity = Convert.ToInt32(TxtRecipeQuantity);

            m_fLogic.AddRecipe(Item, quantity, SelectedComboBoxRecipeItem);
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
        private void Cancel()
        {
            m_currentWindow?.Close();
        }

        /// <summary>
        /// Try creating a new item. Closing the window if successfull, else show error message.
        /// </summary>
        private void CreateItem()
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
            Crafting crafting = (Crafting)Enum.Parse(typeof(Crafting), SelectedCrafting);

            if(m_editing)
                m_fLogic.EditItem(m_item, name, output, time, crafting, PicturePath);
            else
                m_fLogic.AddItem(name, output, time, crafting, PicturePath);


            m_currentWindow?.Close();
        }

        #endregion
    }
}
