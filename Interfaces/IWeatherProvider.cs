using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    public interface IWeatherProvider
    {
        Task<WeatherResult> FetchWeatherDataAsync(double lat, double lon);
    }
}