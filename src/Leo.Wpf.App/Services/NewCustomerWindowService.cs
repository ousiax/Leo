using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Leo.Wpf.App.Services
{
    internal sealed class NewCustomerWindowService(IServiceProvider services) : INewCustomerWindowService
    {
        private readonly IServiceProvider _services = services;

        public bool? ShowDialog()
        {
            var viewModel = _services.GetRequiredService<NewCustomerViewModel>();
            var window = new NewCustomerWindow(viewModel)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            return window.ShowDialog();
        }
    }
}
