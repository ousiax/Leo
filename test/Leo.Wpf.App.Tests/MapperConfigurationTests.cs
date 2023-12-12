using AutoMapper;
using Leo.Windows.Mapper;

namespace Leo.Windows.Tests
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