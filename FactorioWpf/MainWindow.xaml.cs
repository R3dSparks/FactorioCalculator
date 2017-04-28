using Factorio;
using Factorio.Entities;
using System.Windows;
using System.Windows.Controls;

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

        /// <summary>
        /// Event for context menu delete over item in ItemBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDelete_Click(object sender, RoutedEventArgs e)
        {
            logic.RemoveItem((sender as MenuItem)?.Tag as FactorioItem);
            logic.SaveItems();
        }
    }
}
