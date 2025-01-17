﻿// MIT License

using Leo.UI.Services.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Leo.UI.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerDetailService, CustomerDetailService>();

            services.AddHttpClient("Leo", (s, c) =>
            {
                IOptions<WebOptions> addressProvider = s.GetRequiredService<IOptions<WebOptions>>();
                c.BaseAddress = addressProvider.Value.BaseAddress;
            }).AddLoggerHandler(ignoreRequestContent: false, ignoreResponseContent: false);

            return services;
        }
    }
}
