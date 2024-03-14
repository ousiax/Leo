// MIT License

using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Leo.Wpf.App.Services
{
    internal sealed class FindWindowService(IServiceProvider _services, IMessenger _messenger) : IFindWindowService
    {
        public bool? ShowDialog()
        {
            var viewModel = _services.GetRequiredService<FindCustomerViewModel>();
            var window = new FindWindow(viewModel, _messenger)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            return window.ShowDialog();
        }
    }
}
