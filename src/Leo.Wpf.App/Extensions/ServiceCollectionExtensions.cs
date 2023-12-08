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

            services.AddSingleton<CustomerWindow>();
            services.AddSingleton<CustomerViewModel>();

            return services;
        }
    }
}
