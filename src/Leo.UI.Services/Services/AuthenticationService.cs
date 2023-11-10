using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace Leo.UI.Services.Services
{
    sealed class AuthenticationService : IAuthenticationService
    {
        private readonly string[] _scopes = new string[] { "user.read" };
        private readonly IPublicClientApplication _app;

        public AuthenticationService(IOptions<PublicClientApplicationOptions> applicationOptions)
        {
            _app = PublicClientApplicationBuilder.CreateWithApplicationOptions(applicationOptions.Value)
                .Build();
        }

        private AuthenticationResult? _result;

        public async Task<AuthenticationResult> ExecuteAsync()
        {
            if (_result == null)
            {
                var accounts = await _app.GetAccountsAsync();
                try
                {
                    _result = await _app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                                .ExecuteAsync();
                }
                catch (MsalUiRequiredException)
                {
                    _result = await _app.AcquireTokenInteractive(_scopes)
                                .ExecuteAsync();
                }
            }

            return _result;
        }
    }
}
