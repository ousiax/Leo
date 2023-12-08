using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Leo.Wpf.App.Services
{
    internal sealed class NewCustomerWindowService : INewCustomerWindowService
    {
        private readonly IServiceProvider _services;

        public NewCustomerWindowService(IServiceProvider services)
        {
            _services = services;
        }

        public bool? ShowDialog()
        {
            var window = _services.GetRequiredService<NewCustomerWindow>();
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            return window.ShowDialog();
        }
    }
}
