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
    public partial class ItemEditorWindow : Window
    {

        public ItemEditorWindow(IFactorioLogic logic)
        {
            InitializeComponent();

            this.DataContext = new ItemEditorViewModell(logic, this);
        }

        public ItemEditorWindow(IFactorioLogic logic, int id)
        {
            InitializeComponent();

            this.DataContext = new ItemEditorViewModell(logic, this, id);
        }

    }
}
