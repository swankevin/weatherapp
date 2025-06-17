using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetCurrentWeatherAsync(double latitude, double longitude);
    }
}