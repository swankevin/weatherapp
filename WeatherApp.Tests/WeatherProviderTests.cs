using Moq;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Providers;
using WeatherApp.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Moq.Protected;
using System.Threading;

namespace WeatherApp.Tests
{
    public class WeatherProviderTests
    {
        [Fact]
        public async Task FetchWeatherDataAsync_ReturnsWeatherData_OnSuccess()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var expectedJson = "{\"current_weather\":{\"temperature\":25.5,\"windspeed\":10.2,\"winddirection\":180,\"weathercode\":0,\"time\":\"2023-10-27T10:00\"},\"latitude\":-6.97,\"longitude\":110.42,\"generationtime_ms\":0.1,\"utc_offset_seconds\":0,\"timezone\":\"GMT\",\"timezone_abbreviation\":\"GMT\",\"elevation\":0}";
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(expectedJson)
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponse);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var mockLogger = new Mock<ILogger<WeatherProvider>>();

            var provider = new WeatherProvider(httpClient, mockLogger.Object);

            // Act
            var result = await provider.FetchWeatherDataAsync(-6.97, 110.42);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.current_weather);
            Assert.Equal(25.5, result.current_weather.temperature);
            Assert.Equal(10.2, result.current_weather.windspeed);
            Assert.Equal("2023-10-27T10:00", result.current_weather.time);
            Assert.Equal(-6.97, result.latitude);
            Assert.Equal(110.42, result.longitude);
        }
    }
}
