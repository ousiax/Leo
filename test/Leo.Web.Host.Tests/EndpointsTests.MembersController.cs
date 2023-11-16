using Leo.Data.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.Web.Host.Tests
{
    public partial class EndpointsTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task Get_Members_ReturnOK()
        {
            var resp = await _client.GetAsync("/members");

            var members = await resp.Content.ReadFromJsonAsync<IEnumerable<MemberDto>>();

            Assert.NotNull(members);
        }

        [Fact]
        public async Task Post_Members_ReturnCreated()
        {
            var member = new MemberDto
            {
                Name = "Test",
                Gender = Leo.Data.Domain.Entities.Gender.Male,
                CardNo = "test",
                Phone = "test",
            };
            var res = await _client.PostAsJsonAsync("/members", member);

            Assert.Equal(HttpStatusCode.Created, res.StatusCode);

            var result = await res.Content.ReadFromJsonAsync<JsonObject>();
            var id = result!["id"]!.ToString();

            Assert.NotEmpty(id);
        }

        [Fact]
        public async Task Get_MemberById_ReturnOK()
        {
            var member = new MemberDto
            {
                Name = "Test",
                Gender = Leo.Data.Domain.Entities.Gender.Male,
                CardNo = "test",
                Phone = "test",
            };
            var res = await _client.PostAsJsonAsync("/members", member);
            var result = await res.Content.ReadFromJsonAsync<JsonObject>();
            var id = result!["id"]!.ToString();

            res = await _client.GetAsync($"/members/{id}");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}