// MIT License

using System.Windows;
using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Leo.Wpf.App.Services
{
    internal sealed class CustomerEditorWindowService(IServiceProvider _services) : ICustomerEditorWindowService
    {
        public bool? ShowDialog(string customerId)
        {
            CustomerEditorViewModel viewModel = _services.GetRequiredService<CustomerEditorViewModel>();
            _ = viewModel.LoadSelectedCustomerAsync(customerId);
            var window = new CustomerEditorWindow(viewModel)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            return window.ShowDialog();
        }
    }
}
