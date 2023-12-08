using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
        }
    }
}
