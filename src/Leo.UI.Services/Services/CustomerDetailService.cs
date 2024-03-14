// MIT License

using Leo.Data.Domain.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.UI.Services
{
    internal sealed class CustomerDetailService : ICustomerDetailService
    {
        private readonly HttpClient _http;
        private readonly IAuthenticationService _auth;
        private readonly ILogger<CustomerDetailService> _logger;

        public CustomerDetailService(IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService, ILogger<CustomerDetailService> logger)
        {
            _http = httpClientFactory.CreateClient("Leo");
            _auth = authenticationService;
            _logger = logger;

            // TODO
            var auth = _auth.ExecuteAsync().Result;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.IdToken);
        }

        public async Task<CustomerDetailDto?> GetAsync(string id)
        {
            var res = await _http.GetAsync($"/customer-details/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<CustomerDetailDto?>();
        }

        public async Task<List<CustomerDetailDto>> GetByCustomerIdAsync(string customerId)
        {
            List<CustomerDetailDto>? details = null;
            var res = await _http.GetAsync($"/customers/{customerId}/details");
            if (res.IsSuccessStatusCode)
            {
                details = await res.Content.ReadFromJsonAsync<List<CustomerDetailDto>>();
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to read customer details: {}", error);
            }
            return details ?? new();
        }

        public async Task<string?> CreateAsync(CustomerDetailDto detail)
        {
            string? id = null;

            var res = await _http.PostAsJsonAsync("/customer-details", detail);
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadFromJsonAsync<JsonObject>();
                id = result!["id"]!.ToString();
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create customer detail: {}", error);
            }

            return id;
        }
    }
}
