using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App.Services;
using Leo.Wpf.App.ViewModels;
using Leo.Wpf.App.Views;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLeoViewModels(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMessenger, WeakReferenceMessenger>();

            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<CustomerViewModel>();

            services.AddTransient<NewCustomerWindow>();
            services.AddTransient<NewCustomerViewModel>();
            services.AddTransient<INewCustomerWindowService, NewCustomerWindowService>();

            services.AddTransient<FindWindow>();
            services.AddTransient<FindCustomerViewModel>();
            services.AddTransient<IFindWindowService, FindWindowService>();

            return services;
        }
    }
}
