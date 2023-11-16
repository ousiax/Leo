using Leo.Data.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Leo.Web.Host.Tests
{
    public partial class EndpointsTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        const string _memberDetailPathSegment = "/member-details";

        [Fact]
        public async Task Post_MemberDetails_ReturnCreated()
        {
            var memberDetail = new MemberDetailDto
            {
                Id = Guid.NewGuid(),
                MemberId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Item = "test item",
            };
            var res = await _client.PostAsJsonAsync($"{_memberDetailPathSegment}", memberDetail);

            Assert.Equal(HttpStatusCode.Created, res.StatusCode);

            var result = await res.Content.ReadFromJsonAsync<JsonObject>();
            var id = result!["id"]!.ToString();

            Assert.NotEmpty(id);
        }

        [Fact]
        public async Task Get_MemberDetailById_ReturnOK()
        {
            var memberDetail = new MemberDetailDto
            {
                Id = Guid.NewGuid(),
                MemberId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Item = "test item",
            };
            var res = await _client.PostAsJsonAsync(_memberDetailPathSegment, memberDetail);
            var result = await res.Content.ReadFromJsonAsync<JsonObject>();
            var id = result!["id"]!.ToString();

            res = await _client.GetAsync($"{_memberDetailPathSegment}/{id}");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}