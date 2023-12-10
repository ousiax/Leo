using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Leo.Wpf.App.Services
{
    internal sealed class CustomerEditorWindowService(IServiceProvider _services) : ICustomerEditorWindowService
    {
        public bool? ShowDialog(string customerId)
        {
            var viewModel = _services.GetRequiredService<CustomerEditorViewModel>();
            _ = viewModel.LoadSeletedCustomerAsync(customerId);
            var window = new CustomerEditorWindow(viewModel)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            return window.ShowDialog();
        }
    }
}
