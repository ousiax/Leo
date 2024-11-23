// MIT License

using System.Windows;
using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Leo.Wpf.App.Services
{
    internal sealed class EchoWindowService(IServiceProvider _services) : IEchoWindowService
    {
        public void Show()
        {
            EchoViewModel viewModel = _services.GetRequiredService<EchoViewModel>();
            var window = new EchoWindow(viewModel)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.Show();
        }
    }
}
