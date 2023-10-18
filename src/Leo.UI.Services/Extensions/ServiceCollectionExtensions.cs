using Microsoft.Extensions.DependencyInjection;

namespace Leo.UI.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IMemberService, MemberService>();
            services.AddSingleton<IMemberDetailService, MemberDetailService>();
            return services;
        }
    }
}
