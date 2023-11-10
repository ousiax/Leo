using Leo.Data.Domain.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.UI.Services
{
    internal sealed class MemberService : IMemberService
    {
        private readonly HttpClient _http;
        private readonly IAuthenticationService _auth;
        private readonly ILogger<MemberService> _logger;

        public MemberService(IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService, ILogger<MemberService> logger)
        {
            _http = httpClientFactory.CreateClient("Leo");
            _auth = authenticationService;
            _logger = logger;

            // TODO
            var auth = _auth.ExecuteAsync().Result;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.IdToken);
        }

        public async Task<MemberDto?> GetAsync(Guid id)
        {
            var res = await _http.GetAsync($"/members/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<MemberDto?>();
        }

        public async Task<List<MemberDto>> GetAsync()
        {
            var res = await _http.GetAsync($"/members");
            res.EnsureSuccessStatusCode();
            var members = await res.Content.ReadFromJsonAsync<List<MemberDto>>() ?? new();
            return members;
        }

        public async Task<string?> CreateAsync(MemberDto member)
        {
            string? id = null;

            var res = await _http.PostAsJsonAsync("/members", member);
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadFromJsonAsync<JsonObject>();
                id = result!["id"]!.ToString();
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create member: {}", error);
            }

            return id;
        }

        public async Task<int> UpdateAsync(MemberDto member)
        {
            var res = await _http.PutAsJsonAsync("/members", member);
            res.EnsureSuccessStatusCode();
            return 0;
        }
    }
}
