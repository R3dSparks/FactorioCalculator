using PropertyChanged;
using System.ComponentModel;

namespace FactorioWpf
{
    [ImplementPropertyChanged]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public BaseViewModel()
        {

        }
    }
}
