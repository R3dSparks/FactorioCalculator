using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        public void CraftingStations_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            comboBox.ItemsSource = Enum.GetValues(typeof(Crafting));

            comboBox.SelectedIndex = 0;
        }

        public void CraftingStations_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
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
                    return;
                }

                logic.Items.Add(new FactorioItem(name, output, time, crafting));
                logic.WriteFile();
                this.Close();
            }
            if (e.Key == Key.Escape)
            {
                //Close without saving
                this.Close();
            }
        }
    }
}
