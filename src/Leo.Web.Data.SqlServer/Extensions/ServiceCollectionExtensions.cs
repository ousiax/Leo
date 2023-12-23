using Leo.Web.Data.SqlServer.Repositories;
using Leo.Web.Data.SqlServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Leo.Web.Data.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerDetailRepository, CustomerDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddCQRS();
            return services;
        }
    }
}
