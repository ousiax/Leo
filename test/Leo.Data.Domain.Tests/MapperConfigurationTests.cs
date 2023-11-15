using AutoMapper;
using Leo.Data.Domain.Mapper;

namespace Leo.Data.Domain.Tests
{
    public class MapperConfigurationTests
    {
        [Fact]
        public void LeoProfile_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(LeoProfile));
            });
            config.AssertConfigurationIsValid();
        }
    }
}