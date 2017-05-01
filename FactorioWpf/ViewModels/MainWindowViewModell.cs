using Factorio.Entities;
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

        private RelayCommand listBoxItemDelete;

        private RelayCommand listBoxItemEdit;

        private RelayCommand openAddItemDialog;

        #endregion

        #region Public Properties

        public ICommand ListBoxItemEdit
        {
            get
            {
                if (listBoxItemEdit == null)
                    listBoxItemEdit = new RelayCommand(EditItem);

                return listBoxItemEdit;
            }
        }

        /// <summary>
        /// Delete item from the item list
        /// </summary>
        public ICommand ListBoxItemDelete
        {
            get
            {
                if (listBoxItemDelete == null)
                    listBoxItemDelete = new RelayCommand(DeleteItem);

                return listBoxItemDelete;
            }
        }

        /// <summary>
        /// Open a dialog to add a new item
        /// </summary>
        public ICommand OpenAddItemDialog
        {
            get
            {
                if (openAddItemDialog == null)
                    openAddItemDialog = new RelayCommand(OpenAddItem);

                return openAddItemDialog;
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
            editorViewModell.CraftingSelection = SelectedItem.DefaultCraftingStation.ToString();
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
