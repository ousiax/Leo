using Dapper;
using Leo.Web.Data.Services;
using Leo.Web.Data.SQLite.Repositories;
using Leo.Web.Data.SQLite.TypeHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Leo.Web.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            SqlMapper.AddTypeHandler(new GuidToStringHandler());
            //SqlMapper.AddTypeHandler(new GuidAsBinaryHandler());

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
