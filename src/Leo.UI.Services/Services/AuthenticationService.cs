// MIT License

using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace Leo.UI.Services
{
    sealed class AuthenticationService : IAuthenticationService
    {
        private readonly string[] _scopes = new string[] { "openid", "profile", "offline_access" };
        private readonly IPublicClientApplication _app;

        public AuthenticationService(IOptions<PublicClientApplicationOptions> applicationOptions)
        {
            _app = PublicClientApplicationBuilder.CreateWithApplicationOptions(applicationOptions.Value)
                .Build();
        }

        public async Task<AuthenticationResult> ExecuteAsync()
        {
            AuthenticationResult result;
            IEnumerable<IAccount> accounts = await _app.GetAccountsAsync();
            try
            {
                result = await _app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                            .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                result = await _app.AcquireTokenInteractive(_scopes)
                            .ExecuteAsync();
            }

            return result;
        }
    }
}
