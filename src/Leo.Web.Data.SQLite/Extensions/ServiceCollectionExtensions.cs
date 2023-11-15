using Leo.Web.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Leo.Web.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddSingleton<IMemberService, MemberService>();
            services.AddSingleton<IMemberDetailService, MemberDetailService>();
            services.AddCQRS();
            return services;
        }
    }
}
