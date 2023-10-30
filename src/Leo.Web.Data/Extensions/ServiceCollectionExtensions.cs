using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Leo.Web.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
