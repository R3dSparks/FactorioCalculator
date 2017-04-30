using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Factorio.Entities;
using System.Linq;
using FactorioWpf.ViewModels;

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {

        public AddItemWindow(IFactorioLogic logic)
        {
            InitializeComponent();

            this.DataContext = new AddItemViewModell(logic, this);
        }

    }
}
