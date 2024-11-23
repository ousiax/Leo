// MIT License

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Leo.UI.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

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
            AuthenticationResult auth = _auth.ExecuteAsync().Result;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.IdToken);
        }

        public async Task<CustomerDetailDto?> GetAsync(string id)
        {
            HttpResponseMessage res = await _http.GetAsync($"/customer-details/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<CustomerDetailDto?>();
        }

        public async Task<List<CustomerDetailDto>> GetByCustomerIdAsync(string customerId)
        {
            List<CustomerDetailDto>? details = null;
            HttpResponseMessage res = await _http.GetAsync($"/customers/{customerId}/details");
            if (res.IsSuccessStatusCode)
            {
                details = await res.Content.ReadFromJsonAsync<List<CustomerDetailDto>>();
            }
            else
            {
                string error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to read customer details: {}", error);
            }
            return details ?? [];
        }

        public async Task<string?> CreateAsync(CustomerDetailDto detail)
        {
            string? id = null;

            HttpResponseMessage res = await _http.PostAsJsonAsync("/customer-details", detail);
            if (res.IsSuccessStatusCode)
            {
                JsonObject? result = await res.Content.ReadFromJsonAsync<JsonObject>();
                id = result!["id"]!.ToString();
            }
            else
            {
                string error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create customer detail: {}", error);
            }

            return id;
        }
    }
}
