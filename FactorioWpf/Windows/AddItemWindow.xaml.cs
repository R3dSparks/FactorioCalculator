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

            // Add FactorioLogic to the window view modell
            foreach(var x in this.Resources.Values)
            {
                if (x is AddItemViewModell)
                {
                    ((AddItemViewModell)x).Logic = logic;
                    ((AddItemViewModell)x).CurrentWindow = this.AddItemWindowInstance;
                }
                    
            }
        }

    }
}
