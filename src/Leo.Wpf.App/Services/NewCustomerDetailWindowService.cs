// MIT License

using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Leo.Wpf.App.Services
{
    sealed class NewCustomerDetailWindowService(IServiceProvider services, IMessenger messenger) : INewCustomerDetailWindowService
    {
        private readonly IServiceProvider _services = services;

        public bool? ShowDialog(string customerId)
        {
            var viewModel = _services.GetRequiredService<NewCustomerDetailViewModel>();
            viewModel.CustomerId = customerId;
            var window = new NewCustomerDetailWindow(viewModel, messenger)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            return window.ShowDialog();
        }
    }
}
