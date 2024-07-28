using Asp.Versioning;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using SelfLearning.Common;
using SelfLearning.Configurations;

namespace SelfLearning.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [EnableRateLimiting("concurrency")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IOptionsSnapshot<RateLimitingConfiguration> rateLimitingConfig)
        {
            _logger = logger;
        }

        [HttpGet, Route("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet, Route("TestMethod")]
        public WeatherForecast TestMethod(int index)
        {
            try
            {
                if (index < 0 || index >= Summaries.Length - 1)
                    return new WeatherForecast();

                var forecast = new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[index]
                };

                return forecast;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred");
            }
        }
        
    }
}
