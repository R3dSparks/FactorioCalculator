using Factorio;
using Factorio.Entities;
using FactorioWpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModell(new FactorioLogic(@"..\..\..\Factorio.DAL\Files\ItemList.xml"));
        }

        /// <summary>
        /// Close every window from this application if this window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
