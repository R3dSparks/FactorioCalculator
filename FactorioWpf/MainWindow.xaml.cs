using Factorio;
using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFactorioLogic logic = new FactorioLogic(@"..\..\..\Factorio.DAL\Files\ItemList.xml");

        public MainWindow()
        {
            InitializeComponent();

            this.ItemViewerItemsControl.ItemsSource = logic.Items;
        }

        /// <summary>
        /// Opens the Add Item dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow(logic);
            addItemWindow.ShowDialog();
        }

        /// <summary>
        /// Close every window from this application if the main window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
