using Microsoft.AspNetCore.Mvc;

namespace HoneyLovely.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _webhost;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration webhost)
        {
            _webhost = webhost;

            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            var urls = _webhost.GetValue<string>("ASPNETCORE_URLS");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = urls, // Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}