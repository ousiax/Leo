using Leo.UI.Models;
using Leo.UI.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Leo.UI.Services
{
    internal sealed class MemberService : IMemberService
    {
        private readonly HttpClient _http;

        public MemberService(IHttpClientFactory httpClientFactory, IOptions<WebOptions> addressProvider)
        {
            _http = httpClientFactory.CreateClient();
            _http.BaseAddress = addressProvider.Value.BaseAddress;
        }

        public async Task<Member> GetAsync(Guid id)
        {
            var res = await _http.GetAsync($"/members/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Member>();
        }

        public async Task<List<Member>> GetAsync()
        {
            var res = await _http.GetAsync($"/members");
            res.EnsureSuccessStatusCode();
            var members = await res.Content.ReadFromJsonAsync<List<Member>>();
            foreach (var member in members)
            {
                res = await _http.GetAsync($"/members/{member.Id}/details");
                res.EnsureSuccessStatusCode();
                var details = await res.Content.ReadFromJsonAsync<List<MemberDetail>>();
                member.Details.AddRange(details);
            }
            return members;
        }

        public async Task<int> CreateAsync(Member member)
        {
            var res = await _http.PostAsJsonAsync("/members", member);
            res.EnsureSuccessStatusCode();
            return 0;
        }

        public async Task<int> UpdateAsync(Member member)
        {
            var res = await _http.PutAsJsonAsync("/members", member);
            res.EnsureSuccessStatusCode();
            return 0;
        }
    }
}
