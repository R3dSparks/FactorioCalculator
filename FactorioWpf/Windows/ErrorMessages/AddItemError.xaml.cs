using System;
using System.Collections.Generic;
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

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for AddItemError.xaml
    /// </summary>
    public partial class AddItemError : Window
    {
        public AddItemError()
        {
            InitializeComponent();
        }

        public void AddItemErrorOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    
}
