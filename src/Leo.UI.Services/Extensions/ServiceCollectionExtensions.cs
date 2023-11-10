using Leo.UI.Options;
using Leo.UI.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Leo.UI.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMemberService, MemberService>();
            services.AddSingleton<IMemberDetailService, MemberDetailService>();

            services.AddHttpClient("Leo", (s, c) =>
            {
                var addressProvider = s.GetRequiredService<IOptions<WebOptions>>();
                c.BaseAddress = addressProvider.Value.BaseAddress;
            }).AddLoggerHandler(ignoreRequestContent: false, ignoreResponseContent: false);

            return services;
        }
    }
}
