using Factorio.Entities;
using Microsoft.Win32;
using System;
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
        private string[] comboBoxItemCrafting;

        /// <summary>
        /// Logic for interaction with business layer
        /// </summary>
        private IFactorioLogic fLogic;

        /// <summary>
        /// Window this view modell is displayed in
        /// </summary>
        private Window currentWindow;

        /// <summary>
        /// Item id if item is edited
        /// </summary>
        private int itemId;

        /// <summary>
        /// Wether editor is in editing or creating mode
        /// </summary>
        private bool editing = false;

        #endregion

        #region Commands

        /// <summary>
        /// Command for Ok button
        /// </summary>
        private RelayCommand addItemOk_Click;

        /// <summary>
        /// Command for Cancel button
        /// </summary>
        private RelayCommand addItemCancel_Click;

        /// <summary>
        /// Command for Add Picture button
        /// </summary>
        private RelayCommand addItemPicture_Click;

        #endregion

        #region Public Properties

        #region ICommands

        /// <summary>
        /// ICommand binded to the Add Picture button
        /// </summary>
        public ICommand AddItemPicture_Click
        {
            get
            {
                if (addItemPicture_Click == null)
                    addItemPicture_Click = new RelayCommand(AddPicture);

                return addItemPicture_Click;
            }
        }

        /// <summary>
        /// ICommand binded to the Ok button
        /// </summary>
        public ICommand AddItemOk_Click
        {
            get
            {
                if (addItemOk_Click == null)
                    addItemOk_Click = new RelayCommand(CreateItem);

                return addItemOk_Click;
            }
        }

        /// <summary>
        /// ICommand binded to the Cancel button
        /// </summary>
        public ICommand AddItemCancel_Click
        {
            get
            {
                if (addItemCancel_Click == null)
                    addItemCancel_Click = new RelayCommand(Cancel);

                return addItemCancel_Click;
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
                if (comboBoxItemCrafting == null)
                    comboBoxItemCrafting = Enum.GetNames(typeof(Crafting));

                return comboBoxItemCrafting;
            }
        }

        #endregion

        /// <summary>
        /// Currently selected crafting station
        /// </summary>
        public string CraftingSelection { get; set; } = Crafting.AssemblingMachine1.ToString();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ItemEditorViewModell(IFactorioLogic logic, Window window)
        {
            this.fLogic = logic;
            this.currentWindow = window;
        }

        public ItemEditorViewModell(IFactorioLogic logic, Window window, int id)
        {
            this.fLogic = logic;
            this.currentWindow = window;

            this.editing = true;
            this.itemId = id;
        }

        #endregion

        #region Command Methods

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

            PicturePath = fileDialog.FileName;
        }

        /// <summary>
        /// Close the window on cancel click
        /// </summary>
        private void Cancel()
        {
            currentWindow?.Close();
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
                Crafting crafting = (Crafting)Enum.Parse(typeof(Crafting), CraftingSelection);

                if(editing)
                    fLogic.EditItem(itemId, name, output, time, crafting, PicturePath);
                else
                    fLogic.AddItem(name, output, time, crafting, PicturePath);
            }
            catch (Exception ex)
            {
                // Show error message with information about the error
                ErrorMessage errorMessage = new ErrorMessage(ex);
                errorMessage.ShowDialog();
                return;
            }

            currentWindow?.Close();
        }

        #endregion
    }
}
