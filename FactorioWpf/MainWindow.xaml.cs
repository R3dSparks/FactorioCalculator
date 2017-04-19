using Factorio;
using Factorio.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFactorioLogic logic;
        public MainWindow()
        {
            InitializeComponent();

            logic = new FactorioLogic();

            logic.ReadFile("ItemList.xml");

            TreeViewItem item = new TreeViewItem()
            {
                Header = "Items"
            };

            FolderView.Items.Add(item);

            item.Expanded += TreeView_Expanded;

            //Add dummy item
            item.Items.Add(null);


        }

        /// <summary>
        /// Expand the main node to get FactorioItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeItem = sender as TreeViewItem;

            if(treeItem.Items[0] == null)
            {
                //Remove dummy item
                treeItem.Items.RemoveAt(0);

                foreach(var item in logic.Items)
                {
                    var newTreeItem = new TreeViewItem()
                    {
                        Header = item.Name
                    };

                    newTreeItem.Expanded += Item_Expanded;

                    //Add dummy item
                    newTreeItem.Items.Add(null);

                    treeItem.Items.Add(newTreeItem);
                }
            }
        }

        /// <summary>
        /// Expand an item to get the item properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            var item = sender as TreeViewItem;

            if(item.Items[0] == null)
            {
                //Remove dummy item
                item.Items.RemoveAt(0);

                var tempItem = logic.Items.Find(x => x.Name == (string)item.Header);

                item.Items.Add(new TextBlock()
                {                    
                    Text =  $"Output: {tempItem.CraftingOutput} item" + (tempItem.CraftingOutput == 1 ? "" : "s") + "\n" +
                            $"CraftTime: {tempItem.CraftingTime} seconds\n" +
                            $"Productivity: {tempItem.Productivity} items per second\n" +
                            (tempItem.Recipe == null ? "This item has no recipe" : "This item has a recipe"),
                    Foreground = Brushes.Black,                    
                }
                    );
            }
        }
    }
}
