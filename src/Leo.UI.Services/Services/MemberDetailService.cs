using Leo.Data.Domain.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.UI.Services
{
    internal sealed class MemberDetailService : IMemberDetailService
    {
        private readonly HttpClient _http;
        private readonly IAuthenticationService _auth;
        private readonly ILogger<MemberDetailService> _logger;

        public MemberDetailService(IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService, ILogger<MemberDetailService> logger)
        {
            _http = httpClientFactory.CreateClient("Leo");
            _auth = authenticationService;
            _logger = logger;

            // TODO
            var auth = _auth.ExecuteAsync().Result;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.IdToken);
        }

        public async Task<MemberDetailDto?> GetAsync(Guid id)
        {
            var res = await _http.GetAsync($"/member-details/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<MemberDetailDto?>();
        }

        public async Task<List<MemberDetailDto>> GetByMemberIdAsync(Guid memberId)
        {
            List<MemberDetailDto>? details = null;
            var res = await _http.GetAsync($"/members/{memberId}/details");
            if (res.IsSuccessStatusCode)
            {
                details = await res.Content.ReadFromJsonAsync<List<MemberDetailDto>>();
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to read member details: {}", error);
            }
            return details ?? new();
        }

        public async Task<string?> CreateAsync(MemberDetailDto detail)
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
