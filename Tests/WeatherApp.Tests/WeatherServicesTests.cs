using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace WeatherApp.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetCurrentWeatherAsync_ReturnsExpectedResult()
        {
            var mockProvider = new Mock<IWeatherProvider>();
            var mockLogger = new Mock<ILogger<WeatherService>>();

            mockProvider.Setup(p => p.FetchWeatherDataAsync(It.IsAny<double>(), It.IsAny<double>()))
                        .ReturnsAsync(new WeatherResult
                        {
                            current_weather = new WeatherResult.CurrentWeather
                            {
                                temperature = 30.5,
                                windspeed = 15,
                                time = "2025-06-14T10:00"
                            }
                        });

            var service = new WeatherService(mockProvider.Object, mockLogger.Object);
            var result = await service.GetCurrentWeatherAsync(0, 0);

            Assert.Equal(30.5, result.current_weather.temperature);
        }
    }
}
