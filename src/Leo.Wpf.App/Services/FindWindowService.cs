using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Leo.Wpf.App.Services
{
    internal sealed class FindWindowService : IFindWindowService
    {
        private readonly IServiceProvider _services;

        public FindWindowService(IServiceProvider services)
        {
            _services = services;
        }

        public bool? ShowDialog()
        {
            var window = _services.GetRequiredService<FindWindow>();
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            return window.ShowDialog();
        }
    }
}
