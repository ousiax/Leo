using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly CustomerViewModel viewModel;

        public CustomerWindow(CustomerViewModel customerViewModel)
        {
            viewModel = customerViewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
