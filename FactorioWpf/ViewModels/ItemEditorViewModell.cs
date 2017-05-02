﻿using Factorio.Entities;
using Microsoft.Win32;
using System;
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
        private int m_itemId;

        /// <summary>
        /// Wether editor is in editing or creating mode
        /// </summary>
        private bool m_editing = false;

        #endregion

        #region Commands

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
                return m_fLogic.Items;
            }
        }

        #endregion

        /// <summary>
        /// Currently selected crafting station
        /// </summary>
        public string SelectedCrafting { get; set; } = Crafting.AssemblingMachine1.ToString();

        public FactorioItem SelectedRecipeItem { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ItemEditorViewModell(IFactorioLogic logic, Window window)
        {
            init(logic, window);
        }

        public ItemEditorViewModell(IFactorioLogic logic, Window window, int id)
        {
            init(logic, window);

            this.m_editing = true;
            this.m_itemId = id;           
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
            SelectedRecipeItem = m_fLogic.Items[0];
        }

        #endregion

        #region Command Methods

        private void AddRecipeItem()
        {

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
            try
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
                    m_fLogic.EditItem(m_itemId, name, output, time, crafting, PicturePath);
                else
                    m_fLogic.AddItem(name, output, time, crafting, PicturePath);
            }
            catch (Exception ex)
            {
                // Show error message with information about the error
                ErrorMessage errorMessage = new ErrorMessage(ex);
                errorMessage.ShowDialog();
                return;
            }

            m_currentWindow?.Close();
        }

        #endregion
    }
}
