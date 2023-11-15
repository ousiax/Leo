using Leo.Data.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.Web.Host.Tests
{
    public class MembersControllerTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        private readonly LeoWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public MembersControllerTests(LeoWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateDefaultClient();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Leo", "Leo");
        }

        [Fact]
        public async Task Get_HealthCheck_ReturnOK()
        {
            var resp = await _client.GetAsync("/healthz");

            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        }

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
    }
}