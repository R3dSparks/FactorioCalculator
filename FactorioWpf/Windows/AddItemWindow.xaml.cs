using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Factorio.Entities;
using System.Linq;

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
            try
            {
                logic.AddItem(
                    this.AddItemName.Text,
                    this.AddItemOutput.Text,
                    this.AddItemTime.Text,
                    this.AddItemCrafting.SelectedItem);

                this.Close();
            }
            catch (Exception ex)
            {
                var errorMessage = new AddItemError(ex);
                errorMessage.ShowDialog();
            }
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
        /// Called if a key on AddItemWindow is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    try
                    {
                        logic.AddItem(
                            this.AddItemName.Text,
                            this.AddItemOutput.Text,
                            this.AddItemTime.Text,
                            this.AddItemCrafting.SelectedItem);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = new AddItemError(ex);
                        errorMessage.ShowDialog();
                    }
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

    }
}
