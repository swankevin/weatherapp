using WeatherApp.Interfaces;
using WeatherApp.Models;
using System.Net.Http;
using System.Text.Json;

namespace WeatherApp.Providers
{
    public class WeatherProvider : IWeatherProvider
    {
        private readonly HttpClient _http;
        private readonly ILogger<WeatherProvider> _logger;

        public WeatherProvider(HttpClient http, ILogger<WeatherProvider> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<WeatherResult> FetchWeatherDataAsync(double lat, double lon)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&longitude={lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}&current_weather=true";
            _logger.LogInformation("Fetching weather data from URL: {url}", url);

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError("Weather API error: {statusCode}, Details: {content}", response.StatusCode, content);
                throw new HttpRequestException($"Weather API error: {response.StatusCode}, Details: {content}");
            }

            var rawContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Raw Weather API response: {rawContent}", rawContent);

            try
            {
                return JsonSerializer.Deserialize<WeatherResult>(rawContent);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing weather data. Raw content: {rawContent}", rawContent);
                throw new InvalidOperationException("Failed to deserialize weather data.", ex);
            }
        }

    }
}