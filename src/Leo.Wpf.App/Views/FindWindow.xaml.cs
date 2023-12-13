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
        private readonly IMessenger _messenger;

        public FindWindow(FindCustomerViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            _messenger = messenger;

            messenger.Register<FindCustomerViewModel.CloseWindowMessage>(this, (s, e) => this.Close());
        }

        protected override void OnClosed(EventArgs e)
        {
            _messenger.Unregister<FindCustomerViewModel.CloseWindowMessage>(this);
            base.OnClosed(e);
        }
    }
}
