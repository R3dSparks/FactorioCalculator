using System;
using System.Windows;

namespace FactorioWpf
{
    /// <summary>
    /// Interaction logic for AddItemError.xaml
    /// </summary>
    public partial class ErrorMessage : Window
    {
        private Exception exception;

        public ErrorMessage(Exception e)
        {
            InitializeComponent();

            this.exception = e;

            this.ErrorMessageLabel.Content = e.Message;
        }

        public void ErrorMessageOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    
}
