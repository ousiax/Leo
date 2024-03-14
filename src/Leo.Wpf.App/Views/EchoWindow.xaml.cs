// MIT License

using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for EchoWindow.xaml
    /// </summary>
    public partial class EchoWindow : Window
    {
        public EchoWindow(EchoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
