using System.Net;

namespace Leo.Web.Host.Tests
{
    public partial class EndpointsTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task Get_HealthCheck_ReturnOK()
        {
            var resp = await _client.GetAsync("/healthz");

            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        }
    }
}