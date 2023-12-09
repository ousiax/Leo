using CommunityToolkit.Mvvm.Messaging;
using Leo.Wpf.App.Services;
using Leo.Wpf.App.ViewModels;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLeoViewModels(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMessenger, WeakReferenceMessenger>();

            services.AddTransient<IMainWindowService, MainWindowService>();
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<CustomerViewModel>();

            services.AddTransient<NewCustomerViewModel>();
            services.AddTransient<INewCustomerWindowService, NewCustomerWindowService>();

            services.AddTransient<FindCustomerViewModel>();
            services.AddTransient<IFindWindowService, FindWindowService>();

            return services;
        }
    }
}
