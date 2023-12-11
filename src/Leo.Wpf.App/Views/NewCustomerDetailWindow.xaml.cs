using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App.ViewModels;
using System.Windows;

namespace Leo.Wpf.App.Views
{
    /// <summary>
    /// Interaction logic for NewCustomerDetailWindow.xaml
    /// </summary>
    public partial class NewCustomerDetailWindow : Window
    {
        private readonly IMessenger _messenger;

        public NewCustomerDetailWindow(NewCustomerDetailViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();
            viewModel.CloseAction += () => Close();
            this.DataContext = viewModel;
            _messenger = messenger;

            messenger.Register<NewCustomerDetailViewModel.CloseWindowMessage>(this, (s, e) =>
            {
                this.Close();
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            _messenger.UnregisterAll(this);
            base.OnClosed(e);
        }

    }
}
