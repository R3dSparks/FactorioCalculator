using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Factorio.Entities;

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private IFactorioLogic logic;

        public AddItemWindow(IFactorioLogic logic)
        {
            InitializeComponent();

            this.logic = logic;
        }

        #region Events

        /// <summary>
        /// Event for ok button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemOk_Click(object sender, EventArgs e)
        {
            if(AddItem())
                this.Close();
        }

        /// <summary>
        /// Event for cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// Add crafting stations to the combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CraftingStations_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = Enum.GetValues(typeof(Crafting));

            comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Called if a key on AddItemWindow is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    if (AddItem())
                        this.Close();
                    break;
                case Key.Escape:
                    //Close without saving
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Try to create a new item.
        /// </summary>
        /// <returns>True if add was successfull</returns>
        private bool AddItem()
        {
            string name;
            int output;
            double time;
            Crafting crafting;

            try
            {
                name = this.AddItemName.Text;
                output = Convert.ToInt32(this.AddItemOutput.Text);
                time = Convert.ToDouble(this.AddItemTime.Text);
                crafting = (Crafting)this.AddItemCrafting.SelectedItem;
            }
            catch (Exception)
            {
                //If the user input is wrong return to AddItemWindow
                var creatingItemError = new AddItemError();
                creatingItemError.ShowDialog();

                return false;
            }

            logic.Items.Add(new FactorioItem(name, output, time, crafting));
            logic.WriteFile();

            return true;
        }

        #endregion
    }
}
