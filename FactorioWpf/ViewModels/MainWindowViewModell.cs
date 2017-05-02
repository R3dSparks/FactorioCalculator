using Factorio.Entities;
using FactorioWpf.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioWpf.ViewModels
{
    public class MainWindowViewModell : BaseViewModell
    {
        private IFactorioLogic fLogic;

        #region Commands

        private RelayCommand m_deletelistBoxItem;

        private RelayCommand m_openlistBoxItemEdit;

        private RelayCommand m_openAddItemDialog;

        private RelayCommand m_openListBoxItemDetail;

        #endregion

        #region Public Properties

        public ICommand OpenListBoxItemEdit
        {
            get
            {
                if (m_openlistBoxItemEdit == null)
                    m_openlistBoxItemEdit = new RelayCommand(EditItem);

                return m_openlistBoxItemEdit;
            }
        }

        /// <summary>
        /// Delete item from the item list
        /// </summary>
        public ICommand DeleteListBoxItem
        {
            get
            {
                if (m_deletelistBoxItem == null)
                    m_deletelistBoxItem = new RelayCommand(DeleteItem);

                return m_deletelistBoxItem;
            }
        }

        /// <summary>
        /// Open a dialog to add a new item
        /// </summary>
        public ICommand OpenAddItemDialog
        {
            get
            {
                if (m_openAddItemDialog == null)
                    m_openAddItemDialog = new RelayCommand(OpenAddItem);

                return m_openAddItemDialog;
            }
        }

        public ICommand OpenListBoxItemDetail
        {
            get
            {
                if (m_openListBoxItemDetail == null)
                    m_openListBoxItemDetail = new RelayCommand(ItemDetail);

                return m_openListBoxItemDetail;
            }
        }

        /// <summary>
        /// List of all available items
        /// </summary>
        public ObservableCollection<FactorioItem> ItemList
        {
            get
            {
                return fLogic.Items;
            }
        }

        /// <summary>
        /// Currently selected item
        /// </summary>
        public FactorioItem SelectedItem { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModell(IFactorioLogic logic)
        {
            this.fLogic = logic;
        }

        #endregion

        #region Command Methods

        private void ItemDetail()
        {
            ItemDetailWindow itemDetailWindow = new ItemDetailWindow(SelectedItem);

            itemDetailWindow.Show();
        }

        /// <summary>
        /// Opens the Add Item dialog
        /// </summary>
        private void OpenAddItem()
        {
            ItemEditorWindow addItemWindow = new ItemEditorWindow(fLogic);

            ((ItemEditorViewModell)addItemWindow.DataContext).Title = "Add new item";

            addItemWindow.ShowDialog();
        }

        /// <summary>
        /// Edit existing item
        /// </summary>
        private void EditItem()
        {
            ItemEditorWindow editor = new ItemEditorWindow(fLogic, SelectedItem.Id);

            var editorViewModell = (ItemEditorViewModell)editor.DataContext;

            editorViewModell.Title = "Edit item";
            editorViewModell.TxtItemName = SelectedItem.Name;
            editorViewModell.TxtItemOutput = SelectedItem.CraftingOutput.ToString();
            editorViewModell.TxtItemTime = SelectedItem.CraftingTime.ToString();
            editorViewModell.SelectedCrafting = SelectedItem.DefaultCraftingStation.ToString();
            editorViewModell.PicturePath = SelectedItem.PicturePath;

            editor.ShowDialog();
        }

        /// <summary>
        /// Event for context menu delete over item in ItemBox
        /// </summary>
        private void DeleteItem()
        {
            fLogic.RemoveItem(SelectedItem);
        }

        #endregion

    }
}
