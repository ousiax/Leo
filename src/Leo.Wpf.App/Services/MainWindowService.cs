// MIT License

using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;

namespace Leo.Wpf.App.Services
{
    internal sealed class MainWindowService : IMainWindowService
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindowService(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Show()
        {
            var win = new MainWindow(_viewModel);
            win.Show();
        }
    }
}
