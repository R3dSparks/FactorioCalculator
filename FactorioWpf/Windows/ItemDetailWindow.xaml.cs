﻿using Factorio.Entities;
using FactorioWpf.ViewModels;
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

namespace FactorioWpf.Windows
{
    /// <summary>
    /// Interaction logic for ItemDetailWindow.xaml
    /// </summary>
    public partial class ItemDetailWindow : Window
    {
        public ItemDetailWindow(FactorioItem item)
        {
            InitializeComponent();

            this.DataContext = new ItemDetailViewModell(item);
        }
    }
}
