using WeatherApp.Interfaces;
using WeatherApp.Providers;
using WeatherApp.Services;

namespace WeatherApp
{
    public static class ServiceRegister
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddHttpClient<IWeatherProvider, WeatherProvider>();
            services.AddScoped<IWeatherService, WeatherService>();
        }
    }
}
