using Alyio.Extensions;
using Leo.App.Models;
using Leo.App.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Leo.App.Services
{
    internal sealed class MemberDetailService : IMemberDetailService
    {
        private readonly HttpClient _http;

        public MemberDetailService(IHttpClientFactory httpClientFactory, IOptions<WebOptions> addressProvider)
        {
            _http = httpClientFactory.CreateClient();
            _http.BaseAddress = addressProvider.Value.BaseAddress;
        }

        public async Task<int> CreateAsync(MemberDetail detail)
        {
            var response = await _http.PostAsJsonAsync("/member-details", detail);
            response.EnsureSuccessStatusCode();
            var count = await response.Content.ReadAsStringAsync();
            return count.ToInt32().Value;
        }
    }
}
