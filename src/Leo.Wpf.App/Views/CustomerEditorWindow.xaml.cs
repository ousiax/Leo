using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for CustomerEditorWindow.xaml
    /// </summary>
    public partial class CustomerEditorWindow : Window
    {
        public CustomerEditorWindow(CustomerEditorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseAction += () => Close();
        }
    }
}
