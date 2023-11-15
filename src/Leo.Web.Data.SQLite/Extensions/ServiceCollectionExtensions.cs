using Leo.Web.Data.Services;
using Leo.Web.Data.SQLite.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Leo.Web.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberDetailRepository, MemberDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddCQRS();
            return services;
        }
    }
}
