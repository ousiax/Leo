using Leo.UI.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.UI.Services
{
    internal sealed class MemberService : IMemberService
    {
        private readonly HttpClient _http;
        private readonly ILogger<MemberService> _logger;

        public MemberService(IHttpClientFactory httpClientFactory, ILogger<MemberService> logger)
        {
            _http = httpClientFactory.CreateClient("Leo");
            _logger = logger;
        }

        public async Task<Member?> GetAsync(Guid id)
        {
            var res = await _http.GetAsync($"/members/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Member?>();
        }

        public async Task<List<Member>> GetAsync()
        {
            var res = await _http.GetAsync($"/members");
            res.EnsureSuccessStatusCode();
            var members = await res.Content.ReadFromJsonAsync<List<Member>>() ?? new();
            foreach (var member in members)
            {
                res = await _http.GetAsync($"/members/{member.Id}/details");
                res.EnsureSuccessStatusCode();
                var details = await res.Content.ReadFromJsonAsync<List<MemberDetail>>();
                member.Details.AddRange(details);
            }
            return members;
        }

        public async Task<string?> CreateAsync(Member member)
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

        public async Task<int> UpdateAsync(Member member)
        {
            var res = await _http.PutAsJsonAsync("/members", member);
            res.EnsureSuccessStatusCode();
            return 0;
        }
    }
}
