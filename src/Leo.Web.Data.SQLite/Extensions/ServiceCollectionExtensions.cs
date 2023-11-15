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
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddSingleton<IMemberRepository, MemberRepository>();
            services.AddSingleton<IMemberDetailRepository, MemberDetailRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddCQRS();
            return services;
        }
    }
}
