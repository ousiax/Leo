using Leo.UI.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.UI.Services
{
    internal sealed class MemberDetailService : IMemberDetailService
    {
        private readonly HttpClient _http;
        private readonly ILogger<MemberDetailService> _logger;

        public MemberDetailService(IHttpClientFactory httpClientFactory, ILogger<MemberDetailService> logger)
        {
            _http = httpClientFactory.CreateClient("Leo");
            _logger = logger;
        }

        public async Task<string?> CreateAsync(MemberDetail detail)
        {
            string? id = null;

            var res = await _http.PostAsJsonAsync("/member-details", detail);
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadFromJsonAsync<JsonObject>();
                id = result!["id"]!.ToString();
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create member detail: {}", error);
            }

            return id;
        }
    }
}
