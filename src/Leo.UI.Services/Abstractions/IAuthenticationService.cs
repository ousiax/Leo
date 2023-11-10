using Microsoft.Identity.Client;

namespace Leo.UI.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> ExecuteAsync();
    }
}
