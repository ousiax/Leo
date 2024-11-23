// MIT License

namespace Leo.Web.Api.Tests
{
    public partial class EndpointsTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        private readonly LeoWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public EndpointsTests(LeoWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateDefaultClient();
        }
    }
}
