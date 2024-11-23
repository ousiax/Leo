// MIT License

using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Leo.Data.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Leo.Web.Api.Tests
{
    public partial class EndpointsTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        const string _customerDetailPathSegment = "/customer-details";

        [Fact]
        public async Task Post_CustomerDetails_ReturnCreated()
        {
            var customerDetail = new CustomerDetailDto
            {
                Id = Guid.NewGuid().ToString(),
                CustomerId = Guid.NewGuid().ToString(),
                Date = DateTime.UtcNow,
                Item = "test item",
            };
            HttpResponseMessage res = await _client.PostAsJsonAsync($"{_customerDetailPathSegment}", customerDetail);

            Assert.Equal(HttpStatusCode.Created, res.StatusCode);

            JsonObject? result = await res.Content.ReadFromJsonAsync<JsonObject>();
            string id = result!["id"]!.ToString();

            Assert.NotEmpty(id);
        }

        [Fact]
        public async Task Get_CustomerDetailById_ReturnOK()
        {
            var customerDetail = new CustomerDetailDto
            {
                Id = Guid.NewGuid().ToString(),
                CustomerId = Guid.NewGuid().ToString(),
                Date = DateTime.UtcNow,
                Item = "test item",
            };
            HttpResponseMessage res = await _client.PostAsJsonAsync(_customerDetailPathSegment, customerDetail);
            JsonObject? result = await res.Content.ReadFromJsonAsync<JsonObject>();
            string id = result!["id"]!.ToString();

            res = await _client.GetAsync($"{_customerDetailPathSegment}/{id}");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
