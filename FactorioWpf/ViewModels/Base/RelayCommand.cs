using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FactorioWpf.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // Show error message with information about the error
                ErrorMessage errorMessage = new ErrorMessage(ex);
                errorMessage.ShowDialog();
            }
            
        }
    }
}
