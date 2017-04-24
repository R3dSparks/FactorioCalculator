using System.Collections.ObjectModel;
using System.Linq;

namespace FactorioWpf
{
    public class ItemViewerStructureViewModel : BaseViewModel
    {
        public ObservableCollection<FactorioItemViewModel> Items { get; set; }

        public ItemViewerStructureViewModel()
        {
            this.Items = new ObservableCollection<FactorioItemViewModel>(
                ItemViewerStructure.GetItems().Select(item => new FactorioItemViewModel(item))
                );
        }
    }
}
