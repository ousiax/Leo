using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        public FindWindow(FindCustomerViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            messenger.Register<FindCustomerViewModel.CloseWindowMessage>(this, (s, e) => this.Close());
        }
    }
}
