using Microsoft.AspNetCore.Mvc;
using WeatherApp.Interfaces;
using Microsoft.Extensions.Logging; // Added for logging

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _service;
        private readonly ILogger<WeatherController> _logger; // Added for logging

        public WeatherController(IWeatherService service, ILogger<WeatherController> logger) // Modified constructor
        {
            _service = service;
            _logger = logger; // Initialized logger
        }

        public async Task<IActionResult> Index(double lat = -6.97, double lon = 110.42) // Semarang, Indonesia
        {
            _logger.LogInformation("WeatherController Index called with lat: {lat}, lon: {lon}", lat, lon); // Added logging

            if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
            {
                _logger.LogWarning("Invalid latitude or longitude received: lat={lat}, lon={lon}", lat, lon); // Added logging
                return BadRequest("Latitude must be -90 to 90. Longitude must be -180 to 180.");
            }

            var result = await _service.GetCurrentWeatherAsync(lat, lon);
            return View(result);
        }

    }
}
