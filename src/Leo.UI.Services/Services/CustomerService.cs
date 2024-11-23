// MIT License

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Leo.UI.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Leo.UI.Services
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;
        private readonly IAuthenticationService _auth;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IHttpClientFactory httpClientFactory, IAuthenticationService authenticationService, ILogger<CustomerService> logger)
        {
            _http = httpClientFactory.CreateClient("Leo");
            _auth = authenticationService;
            _logger = logger;

            // TODO
            AuthenticationResult auth = _auth.ExecuteAsync().Result;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.IdToken);
        }

        public async Task<CustomerDto?> GetAsync(string id)
        {
            HttpResponseMessage res = await _http.GetAsync($"/customers/{id}");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<CustomerDto?>();
        }

        public async Task<List<CustomerDto>> GetAsync()
        {
            HttpResponseMessage res = await _http.GetAsync($"/customers");
            res.EnsureSuccessStatusCode();
            List<CustomerDto> customers = await res.Content.ReadFromJsonAsync<List<CustomerDto>>() ?? [];
            return customers;
        }

        public async Task<string?> CreateAsync(CustomerDto customer)
        {
            string? id = null;

            HttpResponseMessage res = await _http.PostAsJsonAsync("/customers", customer);
            if (res.IsSuccessStatusCode)
            {
                JsonObject? result = await res.Content.ReadFromJsonAsync<JsonObject>();
                id = result!["id"]!.ToString();
            }
            else
            {
                string error = await res.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create customer: {}", error);
            }

            return id;
        }

        public async Task<int> UpdateAsync(CustomerDto customer)
        {
            HttpResponseMessage res = await _http.PutAsJsonAsync("/customers", customer);
            res.EnsureSuccessStatusCode();
            return 0;
        }
    }
}
