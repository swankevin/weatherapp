using WeatherApp.Interfaces;
using WeatherApp.Models;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherProvider _provider;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(IWeatherProvider provider, ILogger<WeatherService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public async Task<WeatherResult> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            _logger.LogInformation("Fetching weather data for lat={lat}, lon={lon}", latitude, longitude);
            return await _provider.FetchWeatherDataAsync(latitude, longitude);
        }
    }
}