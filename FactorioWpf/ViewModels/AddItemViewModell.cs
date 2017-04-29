using Factorio.Entities;
using System;
using System.Windows;
using System.Windows.Input;

namespace FactorioWpf.ViewModels
{
    public class AddItemViewModell : BaseViewModell
    {
        /// <summary>
        /// Names of <see cref="Crafting"/>
        /// </summary>
        private string[] craftingItemCrafting;

        #region Commands

        /// <summary>
        /// Command for Ok button
        /// </summary>
        private RelayCommand addItemOk_Click;

        /// <summary>
        /// Command for Cancel button
        /// </summary>
        private RelayCommand addItemCancel_Click;

        #endregion

        #region Public Properties

        public Window CurrentWindow { get; set; }

        public IFactorioLogic Logic { get; set; }

        /// <summary>
        /// ICommand to bind to the Ok button
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
        /// ICommand to bind to the Cancel button
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
        public string[] CraftingItemCrafting
        {
            get
            {
                if (craftingItemCrafting == null)
                    craftingItemCrafting = Enum.GetNames(typeof(Crafting));

                return craftingItemCrafting;
            }
        }

        /// <summary>
        /// Currently selected crafting station
        /// </summary>
        public string CraftingSelection { get; set; } = "AssemblingMachine1";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddItemViewModell()
        {

        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Close the window on cancel click
        /// </summary>
        /// <param name="window"></param>
        private void Cancel()
        {
            CurrentWindow?.Close();
        }

        /// <summary>
        /// Try creating a new item. Closing the window if successfull, else show error message.
        /// </summary>
        /// <param name="window">The current window</param>
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

                Logic.AddItem(name, output, time, crafting);
            }
            catch (Exception ex)
            {
                // Show error message with information about the error
                ErrorMessage errorMessage = new ErrorMessage(ex);
                errorMessage.ShowDialog();
                return;
            }

            CurrentWindow?.Close();
        }

        #endregion
    }
}
