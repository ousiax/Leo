// MIT License

using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        private readonly NewCustomerViewModel viewMode;

        public NewCustomerWindow(NewCustomerViewModel viewModel)
        {
            this.viewMode = viewModel;
            this.viewMode.CloseAction += this.Close;
            this.DataContext = viewModel;
            this.InitializeComponent();
        }
    }
}
